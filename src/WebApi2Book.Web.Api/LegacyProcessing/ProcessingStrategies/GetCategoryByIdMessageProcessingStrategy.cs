// GetCategoryByIdMessageProcessingStrategy.cs
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
    public class GetCategoryByIdMessageProcessingStrategy : ILegacyMessageProcessingStrategy
    {
        private readonly ICategoryByIdInquiryProcessor _inquiryProcessor;

        public GetCategoryByIdMessageProcessingStrategy(ICategoryByIdInquiryProcessor inquiryProcessor)
        {
            _inquiryProcessor = inquiryProcessor;
        }

        public object Execute(XElement operationElement)
        {
            XNamespace ns = Constants.DefaultLegacyNamespace;

            var id = PrimitiveTypeParser.Parse<long>(operationElement.Descendants(ns + "categoryId").First().Value);

            var modelCategory = _inquiryProcessor.GetCategory(id);

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof (Category), Constants.DefaultLegacyNamespace);
                serializer.Serialize(stream, modelCategory);

                stream.Seek(0, 0);

                var xDocument = XDocument.Load(stream, LoadOptions.None);
                var categoryAsXElement = xDocument.Descendants(ns + "Category");
                return categoryAsXElement.Elements();
            }
        }

        public bool CanProcess(string operationName)
        {
            return operationName == "GetCategoryById";
        }
    }
}