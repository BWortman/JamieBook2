// UserLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common;
using WebApi2Book.Common.Security;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class UserLinkService : IUserLinkService
    {
        private readonly IUserSession _userSession;

        public UserLinkService(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public void AddLinks(User user)
        {
            AddSelfLink(user);
            AddAllUsersLink(user);
        }

        public virtual void AddSelfLink(User user)
        {
            user.AddLink(new Link
            {
                Title = Constants.CommonLinkTitles.Self,
                Rel = Constants.CommonLinkRelValues.Self,
                Href = string.Format("/api/{0}/users/{1}", _userSession.ApiVersionInUse, user.UserId)
            });
        }

        public virtual Link GetAllUsersLink()
        {
            return new Link
            {
                Title = "All Users",
                Rel = Constants.CommonLinkRelValues.All,
                Href = string.Format("/api/{0}/users", _userSession.ApiVersionInUse)
            };
        }

        public virtual void AddAllUsersLink(User user)
        {
            user.AddLink(GetAllUsersLink());
        }
    }
}