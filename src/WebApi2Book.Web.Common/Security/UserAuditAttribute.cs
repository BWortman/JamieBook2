// UserAuditAttribute.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4net;
using WebApi2Book.Common.Logging;
using WebApi2Book.Common.Security;

namespace WebApi2Book.Web.Common.Security
{
    /// <summary>
    ///     Audits user actions.
    /// </summary>
    public class UserAuditAttribute : ActionFilterAttribute
    {
        private readonly ILog _log;

        public UserAuditAttribute() : this(WebContainerManager.Get<ILogManager>())
        {
        }

        public UserAuditAttribute(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof (UserAuditAttribute));
        }

        public override bool AllowMultiple
        {
            get { return false; }
        }

        public virtual IUserSession UserSession
        {
            get { return WebContainerManager.Get<IUserSession>(); }
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return Task.Run(() => AuditCurrentUser(), cancellationToken);
        }

        public void AuditCurrentUser()
        {
            // Simulate long auditing process
            _log.InfoFormat("Action being executed by user={0}", UserSession.UserId);
            Thread.Sleep(3000);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _log.InfoFormat("Action executed by user={0}", UserSession.UserId);
        }
    }
}