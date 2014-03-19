// Task.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi2Book.Web.Api.Models
{
    public class Task : ILinkContaining, ILocationContaining
    {
        private readonly List<Link> _links = new List<Link>();

        public long TaskId { get; set; }

        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string Subject { get; set; }

        [Editable(true)]
        public DateTime? StartDate { get; set; }

        [Editable(true)]
        public DateTime? DueDate { get; set; }

        [Editable(false)]
        public DateTime? DateCompleted { get; set; }

        [Editable(false)]
        public DateTime CreatedDate { get; set; }

        [Editable(true)]
        [Required]
        public Status Status { get; set; }

        [Editable(true)]
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