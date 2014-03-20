// StatusesInquiryResponse.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class StatusesInquiryResponse
    {
        public StatusesInquiryResponse(IEnumerable<Status> statuses, IEnumerable<Link> links)
        {
            Statuses = statuses ?? new Status[0];
            Links = links ?? new Link[0];
        }

        public IEnumerable<Status> Statuses { get; private set; }
        public IEnumerable<Link> Links { get; private set; }
    }
}