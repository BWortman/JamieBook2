// UserLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common.Security;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class UserLinkService : IUserLinkService
    {
        private readonly ICommonLinkService _commonLinkService;
        private readonly IWebUserSession _userSession;

        public UserLinkService(IWebUserSession userSession, ICommonLinkService commonLinkService)
        {
            _userSession = userSession;
            _commonLinkService = commonLinkService;
        }

        public void AddLinks(User user)
        {
            AddSelfLink(user);
            AddAllUsersLink(user);
        }

        public virtual void AddSelfLink(User user)
        {
            user.AddLink(GetSelfLink(user));
        }

        public virtual Link GetAllUsersLink()
        {
            var path =
                string.Format(
                    Constants.CommonRoutingDefinitions.DelimitedVersionedApiRouteBaseFormatString + "users",
                    _userSession.ApiVersionInUse);
            return _commonLinkService.GetLink(path, Constants.CommonLinkRelValues.All, HttpMethod.Get);
        }

        public virtual void AddAllUsersLink(User user)
        {
            user.AddLink(GetAllUsersLink());
        }

        public virtual Link GetSelfLink(User user)
        {
            var path =
                string.Format(
                    Constants.CommonRoutingDefinitions.DelimitedVersionedApiRouteBaseFormatString + "users/{1}",
                    _userSession.ApiVersionInUse, user.UserId);
            var link = _commonLinkService.GetLink(path, Constants.CommonLinkRelValues.Self, HttpMethod.Get);
            return link;
        }
    }
}