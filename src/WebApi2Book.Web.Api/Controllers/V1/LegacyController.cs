// LegacyController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Xml.Linq;
using WebApi2Book.Web.Api.LegacyProcessing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    public class LegacyController : ApiController
    {
        private readonly ILegacyMessageProcessor _legacyMessageProcessor;

        public LegacyController(ILegacyMessageProcessor legacyMessageProcessor)
        {
            _legacyMessageProcessor = legacyMessageProcessor;
        }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext,
            CancellationToken cancellationToken)
        {
            var requestContentAsString = controllerContext.Request.Content.ReadAsStringAsync().Result;
            var requestContentAsDocument = XDocument.Parse(requestContentAsString);

            var legacyResponse = _legacyMessageProcessor.ProcessLegacyMessage(requestContentAsDocument);

            var responseMsg = controllerContext.Request.CreateResponse(HttpStatusCode.OK, legacyResponse);

            return Task.FromResult(responseMsg);
        }
    }
}