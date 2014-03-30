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
        private readonly IStartTaskWorkflowProcessor _startTaskWorkflowProcessor;

        public TaskWorkflowController(IStartTaskWorkflowProcessor startTaskWorkflowProcessor)
        {
            _startTaskWorkflowProcessor = startTaskWorkflowProcessor;
        }

        [Route("taskActivations/{id:long}", Name = "StartTaskRoute")]
        public Task StartTask(long id)
        {
            var task = _startTaskWorkflowProcessor.StartTask(id);
            return task;
        }
    }
}