// IUserLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public interface IUserLinkService
    {
        void AddLinks(User user);
        void AddSelfLink(User user);
        Link GetAllUsersLink();
        void AddAllUsersLink(User user);
    }
}