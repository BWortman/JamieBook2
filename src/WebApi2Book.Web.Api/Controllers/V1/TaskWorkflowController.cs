// TaskWorkflowController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("")]
    [UnitOfWorkActionFilter]
    public class TaskWorkflowController : ApiController
    {
        private readonly ICompleteTaskWorkflowProcessor _completeTaskWorkflowProcessor;
        private readonly IReactivateTaskWorkflowProcessor _reactivateTaskWorkflowProcessor;
        private readonly IStartTaskWorkflowProcessor _startTaskWorkflowProcessor;

        public TaskWorkflowController(IStartTaskWorkflowProcessor startTaskWorkflowProcessor,
            ICompleteTaskWorkflowProcessor completeTaskWorkflowProcessor,
            IReactivateTaskWorkflowProcessor reactivateTaskWorkflowProcessor)
        {
            _startTaskWorkflowProcessor = startTaskWorkflowProcessor;
            _completeTaskWorkflowProcessor = completeTaskWorkflowProcessor;
            _reactivateTaskWorkflowProcessor = reactivateTaskWorkflowProcessor;
        }

        [Route("taskActivations/{id:long}", Name = "StartTaskRoute")]
        public Task StartTask(long id)
        {
            var task = _startTaskWorkflowProcessor.StartTask(id);
            return task;
        }

        [Route("taskCompletions/{id:long}", Name = "CompleteTaskRoute")]
        public Task CompleteTask(long id)
        {
            var task = _completeTaskWorkflowProcessor.CompleteTask(id);
            return task;
        }

        [Route("taskReactivations/{id:long}", Name = "ReactivateTaskRoute")]
        public Task ReactivateTask(long id)
        {
            var task = _reactivateTaskWorkflowProcessor.ReactivateTask(id);
            return task;
        }
    }
}