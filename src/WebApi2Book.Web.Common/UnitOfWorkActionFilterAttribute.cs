// UnitOfWorkActionFilterAttribute.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi2Book.Web.Common
{
    public class UnitOfWorkActionFilterAttribute : ActionFilterAttribute
    {
        private readonly IActionTransactionHelper _actionTransactionHelper;

        public UnitOfWorkActionFilterAttribute()
            : this(WebContainerManager.Get<IActionTransactionHelper>())
        {
        }

        public UnitOfWorkActionFilterAttribute(IActionTransactionHelper actionTransactionHelper)
        {
            _actionTransactionHelper = actionTransactionHelper;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _actionTransactionHelper.BeginTransaction();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _actionTransactionHelper.EndTransaction(actionExecutedContext);
            _actionTransactionHelper.CloseSession();
        }
    }
}