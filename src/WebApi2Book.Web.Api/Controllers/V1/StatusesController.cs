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
        private readonly IAllStatusesInquiryProcessor _allStatusesInquiryProcessor;
        private readonly IStatusByIdInquiryProcessor _statusByIdInquiryProcessor;

        public StatusesController(IStatusByIdInquiryProcessor statusByIdInquiryProcessor,
            IAllStatusesInquiryProcessor allStatusesInquiryProcessor)
        {
            _statusByIdInquiryProcessor = statusByIdInquiryProcessor;
            _allStatusesInquiryProcessor = allStatusesInquiryProcessor;
        }

        [Route("", Name = "GetStatusesRoute")]
        public IEnumerable<Status> GetStatuses()
        {
            var statuses = _allStatusesInquiryProcessor.GetStatuses();
            return statuses;
        }

        [Route("{id:long}", Name = "GetStatusRoute")]
        public Status GetStatus(long id)
        {
            var status = _statusByIdInquiryProcessor.GetStatus(id);
            return status;
        }
    }
}