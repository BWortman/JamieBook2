// LegacyMessageHandler.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebApi2Book.Web.Api.LegacyProcessing
{
    public class LegacyMessageHandler : DelegatingHandler
    {
        private readonly ILegacyMessageProcessor _legacyMessageProcessor;

        public LegacyMessageHandler(ILegacyMessageProcessor legacyMessageProcessor)
        {
            _legacyMessageProcessor = legacyMessageProcessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var requestContentAsString = request.Content.ReadAsStringAsync().Result;
            var requestContentAsDocument = XDocument.Parse(requestContentAsString);

            var legacyResponse = _legacyMessageProcessor.ProcessLegacyMessage(requestContentAsDocument);

            var responseMsg = request.CreateResponse(HttpStatusCode.OK, legacyResponse);

            return Task.FromResult(responseMsg);
        }
    }
}