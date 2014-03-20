// UsersInquiryResponse.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class UsersInquiryResponse : IPageLinkContaining
    {
        private readonly List<Link> _links = new List<Link>();

        private IEnumerable<User> _users;

        public IEnumerable<User> Users
        {
            get { return _users ?? (_users = new List<User>()); }
            set { _users = value; }
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