// SimpleJsonErrorResult.cs
// Copyright fiserv 2014.

using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace WebApi2Book.Web.Common.ErrorHandling
{
    public class SimpleJsonErrorResult : IHttpActionResult
    {
        private readonly string _errorMessage;
        private readonly HttpRequestMessage _requestMessage;
        private readonly HttpStatusCode _statusCode;

        public SimpleJsonErrorResult(HttpRequestMessage requestMessage, HttpStatusCode statusCode, string errorMessage)
        {
            _requestMessage = requestMessage;
            _statusCode = statusCode;
            _errorMessage = errorMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            dynamic errorContent = new JObject();
            errorContent.errorMessage = _errorMessage;

            var response = new HttpResponseMessage(_statusCode)
            {
                RequestMessage = _requestMessage,
                Content = new StringContent(errorContent.ToString(), Encoding.UTF8)
            };
            return response;
        }
    }
}