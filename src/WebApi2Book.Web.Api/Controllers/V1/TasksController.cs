// TasksController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;
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
        private readonly IAddTaskMaintenanceProcessor _addTaskMaintenanceProcessor;

        public TasksController(IAllTasksInquiryProcessor allTasksInquiryProcessor,
            ITaskByIdInquiryProcessor taskByIdInquiryProcessor,
            IAddTaskMaintenanceProcessor addTaskMaintenanceProcessor)
        {
            _allTasksInquiryProcessor = allTasksInquiryProcessor;
            _taskByIdInquiryProcessor = taskByIdInquiryProcessor;
            _addTaskMaintenanceProcessor = addTaskMaintenanceProcessor;
        }

        [Route("", Name = "GetTasksRoute")]
        public IEnumerable<Task> GetTasks()
        {
            var modelTasks = _allTasksInquiryProcessor.GetTasks();
            return modelTasks;
        }

        [Route("{id:long}", Name = "GetTaskRoute")]
        public Task GetTask(long id)
        {
            var modelTask = _taskByIdInquiryProcessor.GetTask(id);
            return modelTask;
        }

        [Route("", Name = "AddTaskRoute")]
        [HttpPost]
        public IHttpActionResult AddTask(HttpRequestMessage requestMessage, Task task)
        {
            var modelTask = _addTaskMaintenanceProcessor.AddTask(task);

            var result = new ItemCreatedActionResult(requestMessage, modelTask);

            return result;
        }
    }
}