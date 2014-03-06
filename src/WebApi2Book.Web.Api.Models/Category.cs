// Category.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class Category
    {
        private List<Link> _links;
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }
    }
}