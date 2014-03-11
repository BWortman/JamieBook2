// Category.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class Category : ILinkContaining, ILocationContaining
    {
        private readonly List<Link> _links = new List<Link>();
        private bool _shouldSerializeLinks = true;
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Link> Links
        {
            get { return _links; }
        }

        public void AddLink(Link link)
        {
            _links.Add(link);
        }

        public Uri Location
        {
            get { return LocationLinkCalculator.GetLocationLink(this); }
        }

        public bool ShouldSerializeLocation()
        {
            return false;
        }

        public void SetShouldSerializeLinks(bool shouldSerializeLinks)
        {
            _shouldSerializeLinks = shouldSerializeLinks;
        }

        public bool ShouldSerializeLinks()
        {
            return _shouldSerializeLinks;
        }
    }
}