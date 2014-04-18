// PagedTaskDataSecurityMessageHandler.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using WebApi2Book.Common;
using WebApi2Book.Common.Logging;
using WebApi2Book.Common.Security;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using Task = WebApi2Book.Web.Api.Models.Task;

namespace WebApi2Book.Web.Api.Security
{
    public class PagedTaskDataSecurityMessageHandler : DelegatingHandler
    {
        private readonly ILog _log;

        public PagedTaskDataSecurityMessageHandler(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof (PagedTaskDataSecurityMessageHandler));
        }

        public IUserSession UserSession
        {
            get { return WebContainerManager.Get<IUserSession>(); }
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (CanHandleResponse(response))
            {
                ApplySecurityToResponseData((ObjectContent) response.Content);
            }

            return response;
        }

        public bool CanHandleResponse(HttpResponseMessage response)
        {
            var objectContent = response.Content as ObjectContent;
            var canHandleResponse = objectContent != null &&
                                    objectContent.ObjectType == typeof (PagedDataInquiryResponse<Task>);
            return canHandleResponse;
        }

        public void ApplySecurityToResponseData(ObjectContent responseObjectContent)
        {
            var maskData = !UserSession.IsInRole(Constants.RoleNames.SeniorWorker);

            if (maskData)
            {
                _log.DebugFormat("Applying security data masking for user {0}", UserSession.Username);
            }

            ((PagedDataInquiryResponse<Task>) responseObjectContent.Value).Items.ForEach(
                x => x.SetShouldSerializeAssignees(!maskData));
        }
    }
}