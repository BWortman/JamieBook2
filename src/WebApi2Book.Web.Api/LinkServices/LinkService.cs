// LinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using WebApi2Book.Common;
using WebApi2Book.Common.Extensions;
using WebApi2Book.Common.Security;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class LinkService : ILinkService
    {
        private readonly IUserSession _userSession;

        public LinkService(IUserSession userSession)
        {
            _userSession = userSession;
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

        public void AddPageLinks(IPageLinkContaining linkContainer,
            string previousPageQueryString,
            string nextPageQueryString)
        {
            var versionedBaseUri = GetVersionedBaseUri(_userSession.RequestingUri);

            var addPrevPageLink = ShouldAddPreviousPageLink(linkContainer.PageNumber);
            var addNextPageLink = ShouldAddNextPageLink(linkContainer.PageNumber, linkContainer.PageCount);

            if (addPrevPageLink || addNextPageLink)
            {
                if (addPrevPageLink)
                {
                    var uriBuilder = new UriBuilder(versionedBaseUri)
                    {
                        Query = previousPageQueryString
                    };
                    linkContainer.AddLink(GetPreviousPageLink(uriBuilder.Uri));
                }

                if (addNextPageLink)
                {
                    var uriBuilder = new UriBuilder(versionedBaseUri)
                    {
                        Query = nextPageQueryString
                    };
                    linkContainer.AddLink(GetNextPageLink(uriBuilder.Uri));
                }
            }
        }

        public void AddSelfLink(ILinkContaining linkContainer)
        {
            var versionedBaseUri = GetVersionedBaseUri(_userSession.RequestingUri);
            var queryString = _userSession.RequestingUri.QueryWithoutLeadingQuestionMark();
            var uriBuilder =
                new UriBuilder(versionedBaseUri.AbsoluteUri)
                {
                    Query = queryString
                };
            var uriString = uriBuilder.Uri.AbsoluteUri;

            var link = new Link {Href = uriString, Rel = Constants.CommonLinkTitles.Self};
            linkContainer.AddLink(link);
        }

        public virtual Link GetPreviousPageLink(Uri uri)
        {
            return new Link {Href = uri.AbsoluteUri, Rel = Constants.CommonLinkTitles.PreviousPage};
        }

        public virtual Link GetNextPageLink(Uri uri)
        {
            return new Link {Href = uri.AbsoluteUri, Rel = Constants.CommonLinkTitles.NextPage};
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