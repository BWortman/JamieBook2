// AllTasksQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using NHibernate;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AllTasksQueryProcessor : IAllTasksQueryProcessor
    {
        private readonly ISession _session;

        public AllTasksQueryProcessor(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Task> GetTasks()
        {
            var tasks = _session.QueryOver<Task>().List();
            return tasks;
        }
    }
}