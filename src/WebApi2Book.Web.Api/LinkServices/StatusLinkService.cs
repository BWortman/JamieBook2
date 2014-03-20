// StatusLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common.Security;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class StatusLinkService : IStatusLinkService
    {
        private readonly ICommonLinkService _commonLinkService;
        private readonly IWebUserSession _userSession;

        public StatusLinkService(IWebUserSession userSession, ICommonLinkService commonLinkService)
        {
            _userSession = userSession;
            _commonLinkService = commonLinkService;
        }

        public void AddLinks(Status status)
        {
            AddSelfLink(status);
            AddAllStatusesLink(status);
        }

        public virtual void AddSelfLink(Status status)
        {
            status.AddLink(GetSelfLink(status));
        }

        public virtual Link GetAllStatusesLink()
        {
            var path =
                string.Format(
                    Constants.CommonRoutingDefinitions.DelimitedVersionedApiRouteBaseFormatString + "statuses",
                    _userSession.ApiVersionInUse);
            return _commonLinkService.GetLink(path, Constants.CommonLinkRelValues.All, HttpMethod.Get);
        }

        public virtual void AddAllStatusesLink(Status status)
        {
            status.AddLink(GetAllStatusesLink());
        }

        public virtual Link GetSelfLink(Status status)
        {
            var path =
                string.Format(
                    Constants.CommonRoutingDefinitions.DelimitedVersionedApiRouteBaseFormatString + "statuses/{1}",
                    _userSession.ApiVersionInUse, status.StatusId);
            var link = _commonLinkService.GetLink(path, Constants.CommonLinkRelValues.Self, HttpMethod.Get);
            return link;
        }
    }
}