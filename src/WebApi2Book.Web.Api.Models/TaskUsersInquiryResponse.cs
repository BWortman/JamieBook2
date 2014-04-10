// TaskUsersInquiryResponse.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class TaskUsersInquiryResponse
    {
        public TaskUsersInquiryResponse(long taskId)
        {
            TaskId = taskId;
        }

        public long TaskId { get; private set; }

        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Link> Links { get; set; }

        public bool ShouldSerializeTaskId()
        {
            return false;
        }
    }
}