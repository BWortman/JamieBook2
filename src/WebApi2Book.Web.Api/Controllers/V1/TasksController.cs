// TasksController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;
using WebApi2Book.Web.Common.Validation;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    [UnitOfWorkActionFilter]
    public class TasksController : ApiController
    {
        private readonly IAddTaskMaintenanceProcessor _addTaskMaintenanceProcessor;
        private readonly IAllTasksInquiryProcessor _allTasksInquiryProcessor;
        private readonly IDeleteTaskQueryProcessor _deleteTaskQueryProcessor;
        private readonly IPagedDataRequestFactory _pagedDataRequestFactory;
        private readonly ITaskByIdInquiryProcessor _taskByIdInquiryProcessor;
        private readonly IUpdateTaskMaintenanceProcessor _updateTaskMaintenanceProcessor;

        public TasksController(IPagedDataRequestFactory pagedDataRequestFactory,
            IAllTasksInquiryProcessor allTasksInquiryProcessor,
            ITaskByIdInquiryProcessor taskByIdInquiryProcessor,
            IAddTaskMaintenanceProcessor addTaskMaintenanceProcessor,
            IUpdateTaskMaintenanceProcessor updateTaskMaintenanceProcessor,
            IDeleteTaskQueryProcessor deleteTaskQueryProcessor)
        {
            _pagedDataRequestFactory = pagedDataRequestFactory;
            _allTasksInquiryProcessor = allTasksInquiryProcessor;
            _taskByIdInquiryProcessor = taskByIdInquiryProcessor;
            _addTaskMaintenanceProcessor = addTaskMaintenanceProcessor;
            _updateTaskMaintenanceProcessor = updateTaskMaintenanceProcessor;
            _deleteTaskQueryProcessor = deleteTaskQueryProcessor;
        }

        [Route("", Name = "GetTasksRoute")]
        public PagedDataInquiryResponse<Task> GetTasks(HttpRequestMessage requestMessage)
        {
            var request = _pagedDataRequestFactory.Create(requestMessage.RequestUri);

            var tasks = _allTasksInquiryProcessor.GetTasks(request);
            return tasks;
        }

        [Route("{id:long}", Name = "GetTaskRoute")]
        public Task GetTask(long id)
        {
            var task = _taskByIdInquiryProcessor.GetTask(id);
            return task;
        }

        [Route("", Name = "AddTaskRoute")]
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult AddTask(HttpRequestMessage requestMessage, NewTask newTask)
        {
            var task = _addTaskMaintenanceProcessor.AddTask(newTask);

            var result = new ItemCreatedActionResult(requestMessage, task);

            return result;
        }

        [Route("{id:long}", Name = "UpdateTaskRoute")]
        [HttpPut]
        [HttpPatch]
        [ValidateTaskUpdateRequest]
        public Task UpdateTask(HttpRequestMessage requestMessage, long id, [FromBody] object updatedTask)
        {
            var task = _updateTaskMaintenanceProcessor.UpdateTask(id, updatedTask);
            return task;
        }

        [Route("{id:long}", Name = "DeleteTaskRoute")]
        [HttpDelete]
        public HttpResponseMessage DeleteTask(long id)
        {
            _deleteTaskQueryProcessor.DeleteTask(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}