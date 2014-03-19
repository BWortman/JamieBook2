﻿// AddTaskMaintenanceProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class AddTaskMaintenanceProcessor : IAddTaskMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IAddTaskQueryProcessor _queryProcessor;
        private readonly ITaskLinkService _taskLinkService;

        public AddTaskMaintenanceProcessor(IAddTaskQueryProcessor queryProcessor, IAutoMapper autoMapper,
            ITaskLinkService taskLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskLinkService = taskLinkService;
        }

        public Task AddTask(Task task)
        {
            var taskEntity = _autoMapper.Map<Data.Entities.Task>(task);

            _queryProcessor.AddTask(taskEntity);

            var newTask = _autoMapper.Map<Task>(taskEntity);

            _taskLinkService.AddLinks(newTask);

            return newTask;
        }
    }
}