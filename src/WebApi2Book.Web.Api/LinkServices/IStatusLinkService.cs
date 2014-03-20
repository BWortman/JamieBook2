// IStatusLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public interface IStatusLinkService
    {
        void AddLinks(Status status);
        Link GetAllStatusesLink();
        void AddAllStatusesLink(Status status);
        void AddSelfLink(Status status);
    }
}