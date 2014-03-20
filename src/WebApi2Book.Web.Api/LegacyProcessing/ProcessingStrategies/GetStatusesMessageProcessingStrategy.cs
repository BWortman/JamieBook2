// GetStatusesMessageProcessingStrategy.cs
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
    public class GetStatusesMessageProcessingStrategy : ILegacyMessageProcessingStrategy
    {
        private readonly IAllStatusesInquiryProcessor _inquiryProcessor;

        public GetStatusesMessageProcessingStrategy(IAllStatusesInquiryProcessor inquiryProcessor)
        {
            _inquiryProcessor = inquiryProcessor;
        }

        public object Execute(XElement operationElement)
        {
            var modelStatuses = _inquiryProcessor.GetStatuses().Statuses.ToArray();

            XNamespace ns = Constants.DefaultLegacyNamespace;

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof (Status[]), Constants.DefaultLegacyNamespace);
                serializer.Serialize(stream, modelStatuses);

                stream.Seek(0, 0);

                var xDocument = XDocument.Load(stream, LoadOptions.None);
                var categoriesAsXElements = xDocument.Descendants(ns + "Status");
                return categoriesAsXElements;
            }
        }

        public bool CanProcess(string operationName)
        {
            return operationName == "GetStatuses";
        }
    }
}