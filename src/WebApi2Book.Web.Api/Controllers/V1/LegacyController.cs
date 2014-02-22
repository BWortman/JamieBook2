// LegacyController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Legacy;
using Task = System.Threading.Tasks.Task;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    public class LegacyController : ApiController
    {
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext,
            CancellationToken cancellationToken)
        {
            var requestContentAsString = controllerContext.Request.Content.ReadAsStringAsync().Result;
            var requestContentAsDocument = XDocument.Parse(requestContentAsString);

            var legacyResponse = ProcessLegacyMessage(requestContentAsDocument);

            var responseMsg = controllerContext.Request.CreateResponse(HttpStatusCode.OK, legacyResponse);

            return Task.FromResult(responseMsg);
        }

        public virtual LegacyResponse ProcessLegacyMessage(XDocument request)
        {
            // todo - make this method a facade, with chain of command handlers; get rid of the switch.

            //todo - inject
            var _legacyMessageParser = new LegacyMessageParser();


            var operationInputMessage = _legacyMessageParser.GetOperationElement(request);
            var opName = operationInputMessage.Name.LocalName;


            const string OperationName_GetCategories = "GetCategories";

            switch (opName)
            {
                case OperationName_GetCategories:
                    var processingResponse = GetCategories(operationInputMessage);
                    var legacyResponse = new LegacyResponse
                    {
                        Request = request,
                        ProcessingResult = processingResponse
                    };
                    return legacyResponse;
                default:
                    throw new NotSupportedException();
            }
        }

        public virtual object GetCategories(XElement nativeRequestMessage)
        {
            // todo - this is one strategy...

            var categories = new[]
            {
                new Category {Name = "cat1", Description = "category 1"},
                new Category {Name = "cat2", Description = "category 2"}
            };

            const string nsAsString = "http://tempuri.org/";
            XNamespace ns = nsAsString;

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof (Category[]), nsAsString);
                serializer.Serialize(stream, categories);

                stream.Seek(0, 0);

                var xDocument = XDocument.Load(stream, LoadOptions.None);
                var categoriesAsXElements = xDocument.Descendants(ns + "Category");
                return categoriesAsXElements;
            }
        }
    }
}