// ITaskUsersMaintenanceProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface ITaskUsersMaintenanceProcessor
    {
        TaskUsersInquiryResponse ReplaceTaskUsers(long taskId, IEnumerable<long> userIds);
        TaskUsersInquiryResponse DeleteTaskUsers(long taskId);
    }
}