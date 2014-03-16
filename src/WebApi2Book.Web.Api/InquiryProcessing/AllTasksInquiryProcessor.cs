// AllTasksInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class AllTasksInquiryProcessor : IAllTasksInquiryProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IAllTasksQueryProcessor _queryProcessor;
        private readonly ITaskLinkService _taskLinkService;

        public AllTasksInquiryProcessor(IAllTasksQueryProcessor queryProcessor, IAutoMapper autoMapper,
            ITaskLinkService taskLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskLinkService = taskLinkService;
        }

        public IEnumerable<Task> GetTasks()
        {
            var tasks = _queryProcessor.GetTasks();

            var modelTasks = tasks.Select(x => _autoMapper.Map<Task>(x)).ToList();

            modelTasks.ForEach(x => _taskLinkService.AddLinks(x));

            return modelTasks;
        }
    }
}