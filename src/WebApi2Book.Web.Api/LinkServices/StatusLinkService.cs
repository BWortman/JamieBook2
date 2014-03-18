// StatusLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common;
using WebApi2Book.Common.Security;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class StatusLinkService : IStatusLinkService
    {
        private readonly IUserSession _userSession;

        public StatusLinkService(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public void AddLinks(Status modelStatus)
        {
            AddSelfLink(modelStatus);
            AddAllStatusesLink(modelStatus);
        }

        public virtual void AddSelfLink(Status modelStatus)
        {
            modelStatus.AddLink(new Link
            {
                Title = Constants.CommonLinkTitles.Self,
                Rel = Constants.CommonLinkRelValues.Self,
                Href = string.Format("/api/{0}/statuses/{1}", _userSession.ApiVersionInUse, modelStatus.StatusId)
            });
        }

        public virtual void AddAllStatusesLink(Status modelStatus)
        {
            modelStatus.AddLink(new Link
            {
                Title = "All Statuses",
                Rel = Constants.CommonLinkRelValues.All,
                Href = string.Format("/api/{0}/statuses", _userSession.ApiVersionInUse)
            });
        }
    }
}