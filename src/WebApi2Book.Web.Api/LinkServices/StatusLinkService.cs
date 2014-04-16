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

        public virtual Link GetAllStatusesLink()
        {
            var path =
                string.Format(
                    Constants.CommonRoutingDefinitions.DelimitedVersionedApiRouteBaseFormatString + "statuses",
                    _userSession.ApiVersionInUse);
            return _commonLinkService.GetLink(path, "statuses", HttpMethod.Get);
        }
    }
}