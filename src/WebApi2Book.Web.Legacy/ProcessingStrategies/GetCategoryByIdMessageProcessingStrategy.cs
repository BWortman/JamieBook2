// GetCategoryByIdMessageProcessingStrategy.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Legacy.ProcessingStrategies
{
    public class GetCategoryByIdMessageProcessingStrategy : ILegacyMessageProcessingStrategy
    {
        public object Execute(XElement operationElement)
        {
            XNamespace ns = Constants.DefaultNamespace;

            // todo - fetch from db
            var id = PrimitiveTypeParser.Parse<int>(operationElement.Descendants(ns + "categoryId").First().Value);
            var category = new Category {CategoryId = id, Name = "cat" + id, Description = "category " + id};

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof (Category), Constants.DefaultNamespace);
                serializer.Serialize(stream, category);

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