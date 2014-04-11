// Status.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class Status : ILinkContaining,  ILocationContaining
    {
        private List<Link> _links;

        public long StatusId { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value;}
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
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