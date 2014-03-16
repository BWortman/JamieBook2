// TasksController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Web.Http;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    [UnitOfWorkActionFilter]
    public class TasksController : ApiController
    {
        private readonly IAllTasksInquiryProcessor _allTasksInquiryProcessor;
        private readonly ITaskByIdInquiryProcessor _taskByIdInquiryProcessor;

        public TasksController(IAllTasksInquiryProcessor allTasksInquiryProcessor,
            ITaskByIdInquiryProcessor taskByIdInquiryProcessor)
        {
            _allTasksInquiryProcessor = allTasksInquiryProcessor;
            _taskByIdInquiryProcessor = taskByIdInquiryProcessor;
        }

        [Route("", Name = "GetTasksRoute")]
        public IEnumerable<Task> GetTasks()
        {
            var modelTasks = _allTasksInquiryProcessor.GetTasks();
            return modelTasks;
        }

        [Route("{id:long}", Name = "GetTaskRoute")]
        public Task GetUser(long id)
        {
            var modelTask = _taskByIdInquiryProcessor.GetTask(id);
            return modelTask;
        }
    }
}