// ISecurityService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Web.Api.Security
{
    public interface ISecurityService
    {
        bool SetPrincipal(string username, string password);
    }
}