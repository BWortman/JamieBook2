// TaskUsersInquiryResponse.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class TaskUsersInquiryResponse
    {
        public long TaskId { get; set; }

        public List<User> Users { get; set; }
        public List<Link> Links { get; set; }

        public bool ShouldSerializeTaskId()
        {
            return false;
        }
    }
}