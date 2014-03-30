// UpdateTaskStatusQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using WebApi2Book.Common;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class UpdateTaskStatusQueryProcessor : IUpdateTaskStatusQueryProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;

        public UpdateTaskStatusQueryProcessor(ISession session, IDateTime dateTime)
        {
            _session = session;
            _dateTime = dateTime;
        }

        public void UpdateTaskStatus(Task taskToUpdate, string statusName)
        {
            var status = _session.QueryOver<Status>().Where(x => x.Name == statusName).SingleOrDefault();

            taskToUpdate.Status = status;
            taskToUpdate.StartDate = _dateTime.UtcNow;

            _session.SaveOrUpdate(taskToUpdate);
        }
    }
}