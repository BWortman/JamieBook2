// TaskByIdInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class TaskByIdInquiryProcessor : ITaskByIdInquiryProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ITaskByIdQueryProcessor _queryProcessor;
        private readonly ITaskLinkService _taskLinkService;

        public TaskByIdInquiryProcessor(ITaskByIdQueryProcessor queryProcessor, IAutoMapper autoMapper,
            ITaskLinkService taskLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskLinkService = taskLinkService;
        }

        public Task GetTask(long taskId)
        {
            var task = _queryProcessor.GetTask(taskId);
            if (task == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }

            var modelTask = _autoMapper.Map<Task>(task);

            _taskLinkService.AddLinks(modelTask);

            return modelTask;
        }
    }
}