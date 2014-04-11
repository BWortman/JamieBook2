// ILinkContaining.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public interface ILinkContaining
    {
        List<Link> Links { get; }
        void AddLink(Link link);
    }
}