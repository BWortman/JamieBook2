// GetCategoriesMessageProcessingStrategy.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Legacy.ProcessingStrategies
{
    public class GetCategoriesMessageProcessingStrategy : ILegacyMessageProcessingStrategy
    {
        public object Execute(XElement operationElement)
        {
            // todo - fetch from db
            var categories = new[]
            {
                new Category {CategoryId = 1, Name = "cat1", Description = "category 1"},
                new Category {CategoryId = 2, Name = "cat2", Description = "category 2"}
            };

            XNamespace ns = Constants.DefaultNamespace;

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof (Category[]), Constants.DefaultNamespace);
                serializer.Serialize(stream, categories);

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