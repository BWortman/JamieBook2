// PagedDataInquiryResponse.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class PagedDataInquiryResponse<T> : IPageLinkContaining
    {
        private readonly List<Link> _links = new List<Link>();

        private IEnumerable<T> _items;

        public IEnumerable<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }

        public int PageSize { get; set; }

        public IEnumerable<Link> Links
        {
            get { return _links; }
        }

        public void AddLink(Link link)
        {
            _links.Add(link);
        }

        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}