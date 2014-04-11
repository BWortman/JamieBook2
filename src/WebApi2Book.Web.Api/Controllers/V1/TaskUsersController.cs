// TaskUsersController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Web.Http;
using NHibernate.Proxy;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    [UnitOfWorkActionFilter]
    public class TaskUsersController : ApiController
    {
        private readonly ITaskUsersInquiryProcessor _taskUsersInquiryProcessor;
        private readonly ITaskUsersMaintenanceProcessor _taskUsersMaintenanceProcessor;

        public TaskUsersController(ITaskUsersInquiryProcessor taskUsersInquiryProcessor,
            ITaskUsersMaintenanceProcessor taskUsersMaintenanceProcessor)
        {
            _taskUsersInquiryProcessor = taskUsersInquiryProcessor;
            _taskUsersMaintenanceProcessor = taskUsersMaintenanceProcessor;
        }

        [Route("{taskId:long}/users", Name = "GetTaskUsersRoute")]
        public TaskUsersInquiryResponse GetTaskUsers(long taskId)
        {
            var users = _taskUsersInquiryProcessor.GetTaskUsers(taskId);
            return users;
        }

        [Route("{taskId:long}/users", Name = "ReplaceTaskUsersRoute")]
        [HttpPut]
        public TaskUsersInquiryResponse UpdateTaskUsers(long taskId, [FromBody] IEnumerable<long> userIds)
        {
            var users = _taskUsersMaintenanceProcessor.ReplaceTaskUsers(taskId, userIds);
            return users;
        }

        [Route("{taskId:long}/users", Name = "DeleteTaskUsersRoute")]
        [HttpDelete]
        public TaskUsersInquiryResponse DeleteTaskUsers(long taskId)
        {
            var users = _taskUsersMaintenanceProcessor.DeleteTaskUsers(taskId);
            return users;
        }
    }
}