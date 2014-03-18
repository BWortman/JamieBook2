// AddTaskQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using NHibernate.Util;
using WebApi2Book.Common;
using WebApi2Book.Common.Security;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AddTaskQueryProcessor : IAddTaskQueryProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        private readonly IUserSession _userSession;

        public AddTaskQueryProcessor(ISession session, IUserSession userSession, IDateTime dateTime)
        {
            _session = session;
            _userSession = userSession;
            _dateTime = dateTime;
        }

        public void AddTask(Task task)
        {
            task.CreatedDate = _dateTime.UtcNow;
            task.Status = _session.Get<Status>(task.Status.StatusId);
            task.CreatedBy = _session.Get<User>(_userSession.UserId);

            if (task.Users != null && task.Users.Any())
            {
                foreach (var user in task.Users)
                {
                    _session.Get<User>(user.UserId);
                }
            }

            _session.SaveOrUpdate(task);
        }
    }
}