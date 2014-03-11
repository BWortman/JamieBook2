// GetCategoriesMessageProcessingStrategy.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LegacyProcessing.ProcessingStrategies
{
    public class GetCategoriesMessageProcessingStrategy : ILegacyMessageProcessingStrategy
    {
        private readonly IAllCategoriesInquiryProcessor _inquiryProcessor;

        public GetCategoriesMessageProcessingStrategy(IAllCategoriesInquiryProcessor inquiryProcessor)
        {
            _inquiryProcessor = inquiryProcessor;
        }

        public object Execute(XElement operationElement)
        {
            var modelCategories = _inquiryProcessor.GetCategories().ToArray();
            Array.ForEach(modelCategories, x => x.SetShouldSerializeLinks(false));

            XNamespace ns = Constants.DefaultLegacyNamespace;

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof (Category[]), Constants.DefaultLegacyNamespace);
                serializer.Serialize(stream, modelCategories);

                stream.Seek(0, 0);

                var xDocument = XDocument.Load(stream, LoadOptions.None);
                var categoriesAsXElements = xDocument.Descendants(ns + "Category");
                return categoriesAsXElements;
            }
        }

        public bool CanProcess(string operationName)
        {
            return operationName == "GetCategories";
        }
    }
}