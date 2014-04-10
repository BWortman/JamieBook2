// TaskUsersController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http;
using WebApi2Book.Web.Api.InquiryProcessing;
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

        public TaskUsersController(ITaskUsersInquiryProcessor taskUsersInquiryProcessor)
        {
            _taskUsersInquiryProcessor = taskUsersInquiryProcessor;
        }

        [Route("{taskId:long}/users", Name = "GetTaskUsersRoute")]
        public TaskUsersInquiryResponse GetTaskUsers(long taskId)
        {
            var users = _taskUsersInquiryProcessor.GetTaskUsers(taskId);
            return users;
        }
    }
}