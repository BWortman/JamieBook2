// TaskAddingProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using NHibernate;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class TaskAddingProcessor : ITaskAddingProcessor
    {
        //private readonly IAutoMapper _autoMapper;
        //private readonly ITaskLinkService _taskLinkService;
        //private readonly ISession _session;

        //public TaskAddingProcessor(ISession session, IAutoMapper autoMapper, ITaskLinkService taskLinkService)
        //{
        //    _session = session;
        //    _autoMapper = autoMapper;
        //    _taskLinkService = taskLinkService;
        //}

        public Task AddTask(Task modelTask)
        {
            throw new NotImplementedException("work in progress... this was salvaged from the categories controller");

            //var task = _autoMapper.Map<Data.Entities.Task>(modelTask);
            //_session.Save(task);

            //var newModelTask = _autoMapper.Map<Task>(task);
            //_taskLinkService.AddLinks(newModelTask);

            //return newModelTask;
        }
    }
}