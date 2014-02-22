// LegacyController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Xml.Linq;
using WebApi2Book.Web.Legacy;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    public class LegacyController : ApiController
    {
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext,
            CancellationToken cancellationToken)
        {
            var requestContentAsString = controllerContext.Request.Content.ReadAsStringAsync().Result;
            var requestContentAsDocument = XDocument.Parse(requestContentAsString);


            // todo: inject
            var legacyResponse = new LegacyMessageProcessor().ProcessLegacyMessage(requestContentAsDocument);

            var responseMsg = controllerContext.Request.CreateResponse(HttpStatusCode.OK, legacyResponse);

            return Task.FromResult(responseMsg);
        }
    }
}