// GetStatusByIdMessageProcessingStrategy.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LegacyProcessing.ProcessingStrategies
{
    public class GetStatusByIdMessageProcessingStrategy : ILegacyMessageProcessingStrategy
    {
        private readonly IStatusByIdInquiryProcessor _inquiryProcessor;

        public GetStatusByIdMessageProcessingStrategy(IStatusByIdInquiryProcessor inquiryProcessor)
        {
            _inquiryProcessor = inquiryProcessor;
        }

        public object Execute(XElement operationElement)
        {
            XNamespace ns = Constants.DefaultLegacyNamespace;

            var id = PrimitiveTypeParser.Parse<long>(operationElement.Descendants(ns + "statusId").First().Value);

            var modelStatus = _inquiryProcessor.GetStatus(id);

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof (Status), Constants.DefaultLegacyNamespace);
                serializer.Serialize(stream, modelStatus);

                stream.Seek(0, 0);

                var xDocument = XDocument.Load(stream, LoadOptions.None);
                var categoryAsXElement = xDocument.Descendants(ns + "Status");
                return categoryAsXElement.Elements();
            }
        }

        public bool CanProcess(string operationName)
        {
            return operationName == "GetStatusById";
        }
    }
}