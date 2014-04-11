// TaskUsersMaintenanceProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class TaskUsersMaintenanceProcessor : ITaskUsersMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IUpdateTaskQueryProcessor _queryProcessor;
        private readonly ITaskUsersLinkService _taskUsersLinkService;

        public TaskUsersMaintenanceProcessor(IUpdateTaskQueryProcessor queryProcessor, IAutoMapper autoMapper,
            ITaskUsersLinkService taskUsersLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskUsersLinkService = taskUsersLinkService;
        }

        public TaskUsersInquiryResponse ReplaceTaskUsers(long taskId, IEnumerable<long> userIds)
        {
            var taskEntity = _queryProcessor.ReplaceTaskUsers(taskId, userIds);

            var task = _autoMapper.Map<Task>(taskEntity);

            var inquiryResponse = new TaskUsersInquiryResponse(taskId) {Users = task.Assignees};

            _taskUsersLinkService.AddLinks(inquiryResponse);

            return inquiryResponse;
        }
    }
}