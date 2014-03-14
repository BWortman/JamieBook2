// Status.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class Status : ILinkContaining, ILocationContaining
    {
        private readonly List<Link> _links = new List<Link>();

        public long StatusId { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }

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
    }
}