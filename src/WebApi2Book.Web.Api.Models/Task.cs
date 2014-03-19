// Task.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class Task : ILinkContaining, ILocationContaining
    {
        private readonly List<Link> _links = new List<Link>();

        public long TaskId { get; set; }
        public string Subject { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }
        public List<User> Assignees { get; set; }

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