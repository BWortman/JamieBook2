// StatusesInquiryResponse.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class StatusesInquiryResponse
    {
        public StatusesInquiryResponse(IEnumerable<Status> statuses, IEnumerable<Link> links)
        {
            Statuses = statuses;
            Links = links;
        }

        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<Link> Links { get; set; }
    }
}