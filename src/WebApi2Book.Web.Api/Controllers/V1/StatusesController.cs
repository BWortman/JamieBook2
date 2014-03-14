// StatusesController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Web.Http;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("statuses")]
    [UnitOfWorkActionFilter]
    public class StatusesController : ApiController
    {
        // TODO: Use the block approach in TasksController, but not here b/c it's overkill
        private readonly IStatusesInquiryProcessorBlock _statusesInquiryProcessorBlock;

        public StatusesController(IStatusesInquiryProcessorBlock statusesInquiryProcessorBlock)
        {
            _statusesInquiryProcessorBlock = statusesInquiryProcessorBlock;
        }

        [Route("", Name = "GetStatusesRoute")]
        public IEnumerable<Status> GetStatuses()
        {
            var modelStatuses = _statusesInquiryProcessorBlock.AllStatusesInquiryProcessor.GetStatuses();
            return modelStatuses;
        }

        [Route("{id:long}", Name = "GetStatusRoute")]
        public Status GetStatus(long id)
        {
            var modelStatus = _statusesInquiryProcessorBlock.StatusByIdInquiryProcessor.GetStatus(id);
            return modelStatus;
        }


        // TODO: Use this approach for Task!
        ////[AdministratorAuthorized]
        //[Route("", Name = "AddTaskRoute")]
        //[HttpPost]
        //public IHttpActionResult Add(HttpRequestMessage request, Task task)
        //{
        //    var newModelTask = _tasksMaintenanceProcessorBlock.TaskAddingProcessor.AddTask(task);
        //    return new ItemCreatedActionResult(request, newModelTask);
        //}
    }
}