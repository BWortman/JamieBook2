// CommonLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Net.Http;
using WebApi2Book.Common;
using WebApi2Book.Common.Extensions;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common.Security;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class CommonLinkService : ICommonLinkService
    {
        private readonly IWebUserSession _userSession;

        public CommonLinkService(IWebUserSession userSession)
        {
            _userSession = userSession;
        }

        public virtual Link GetLink(string path, string relValue, HttpMethod httpMethod)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = _userSession.RequestUri.Scheme,
                Host = _userSession.RequestUri.Host,
                Port = _userSession.RequestUri.Port,
                Path = path
            };

            var link = new Link
            {
                Href = uriBuilder.Uri.AbsoluteUri,
                Rel = relValue,
                Method = httpMethod.Method
            };
            return link;
        }

        public void AddPageLinks(IPageLinkContaining linkContainer,
            string currentPageQueryString,
            string previousPageQueryString,
            string nextPageQueryString)
        {
            var versionedBaseUri = GetVersionedBaseUri(_userSession.RequestUri);

            AddCurrentPageLink(linkContainer, versionedBaseUri, currentPageQueryString);

            var addPrevPageLink = ShouldAddPreviousPageLink(linkContainer.PageNumber);
            var addNextPageLink = ShouldAddNextPageLink(linkContainer.PageNumber, linkContainer.PageCount);

            if (addPrevPageLink || addNextPageLink)
            {
                if (addPrevPageLink)
                {
                    AddPreviousPageLink(linkContainer, versionedBaseUri, previousPageQueryString);
                }

                if (addNextPageLink)
                {
                    AddNextPageLink(linkContainer, versionedBaseUri, nextPageQueryString);
                }
            }
        }

        public virtual void AddCurrentPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var currentPageUriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };
            linkContainer.AddLink(GetCurrentPageLink(currentPageUriBuilder.Uri));
        }

        public virtual void AddPreviousPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var uriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };
            linkContainer.AddLink(GetPreviousPageLink(uriBuilder.Uri));
        }

        public virtual void AddNextPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var uriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };
            linkContainer.AddLink(GetNextPageLink(uriBuilder.Uri));
        }

        public virtual Uri GetVersionedBaseUri(Uri uri)
        {
            var requestingBaseUriString = uri.GetBaseUri().AbsoluteUri;
            var fullyDelimitedVersionSegment = string.Format("/{0}/", _userSession.ApiVersionInUse);
            var versionSegmentStartIndex = requestingBaseUriString.IndexOf(fullyDelimitedVersionSegment,
                StringComparison.Ordinal);

            if (versionSegmentStartIndex < 0)
            {
                var fullyDelimitedApiSegmentName = string.Format("/{0}/",
                    Constants.CommonRoutingDefinitions.ApiSegmentName);
                var fullyDelimitedApiSegmentLength = fullyDelimitedApiSegmentName.Length;
                var fullyDelimitedApiSegmentIndex = requestingBaseUriString.IndexOf(fullyDelimitedApiSegmentName,
                    StringComparison.Ordinal);
                var versionUri =
                    string.Concat(
                        requestingBaseUriString.Substring(0,
                            fullyDelimitedApiSegmentIndex + fullyDelimitedApiSegmentLength - 1),
                        fullyDelimitedVersionSegment,
                        requestingBaseUriString.Substring(fullyDelimitedApiSegmentIndex + fullyDelimitedApiSegmentLength));
                return new Uri(versionUri);
            }
            return uri.GetBaseUri();
        }

        public virtual Link GetCurrentPageLink(Uri uri)
        {
            return new Link
            {
                Href = uri.AbsoluteUri,
                Rel = Constants.CommonLinkRelValues.CurrentPage,
                Method = HttpMethod.Get.Method
            };
        }

        public virtual Link GetPreviousPageLink(Uri uri)
        {
            return new Link
            {
                Href = uri.AbsoluteUri,
                Rel = Constants.CommonLinkRelValues.PreviousPage,
                Method = HttpMethod.Get.Method
            };
        }

        public virtual Link GetNextPageLink(Uri uri)
        {
            return new Link
            {
                Href = uri.AbsoluteUri,
                Rel = Constants.CommonLinkRelValues.NextPage,
                Method = HttpMethod.Get.Method
            };
        }

        public bool ShouldAddPreviousPageLink(int pageNumber)
        {
            return pageNumber > 1;
        }

        public bool ShouldAddNextPageLink(int pageNumber, int pageCount)
        {
            return pageNumber < pageCount;
        }
    }
}