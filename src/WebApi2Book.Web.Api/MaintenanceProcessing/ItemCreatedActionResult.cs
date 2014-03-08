// ItemCreatedActionResult.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi2Book.Web.Api.Models;
using Task = System.Threading.Tasks.Task;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class ItemCreatedActionResult : IHttpActionResult
    {
        private readonly ILocationContaining _createdItem;
        private readonly HttpRequestMessage _requestMessage;

        public ItemCreatedActionResult(HttpRequestMessage requestMessage, ILocationContaining createdItem)
        {
            _requestMessage = requestMessage;
            _createdItem = createdItem;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.Created, _createdItem);
            responseMessage.Headers.Location = _createdItem.Location;

            return responseMessage;
        }
    }
}