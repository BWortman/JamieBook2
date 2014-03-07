// ILinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public interface ILinkService
    {
        /// <summary>
        ///     Gets the versioned base (i.e., excluding query string) portion of the uri.
        /// </summary>
        Uri GetVersionedBaseUri(Uri uri);

        void AddPageLinks(IPageLinkContaining linkContainer,
            string previousPageQueryString,
            string nextPageQueryString);

        void AddSelfLink(ILinkContaining linkContainer);
    }
}