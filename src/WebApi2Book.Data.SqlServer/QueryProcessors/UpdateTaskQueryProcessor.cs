using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Util;
using WebApi2Book.Common;
using WebApi2Book.Common.Security;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.Exceptions;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class UpdateTaskQueryProcessor : IUpdateTaskQueryProcessor
    {
        private readonly ISession _session;

        public UpdateTaskQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Task GetUpdatedTask(long taskId, Dictionary<string, object> updatedPropertyValueMap)
        {
            var task = _session.Get<Task>(taskId);
            if (task == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }

            var propertyInfos = typeof(Task).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyValuePair in updatedPropertyValueMap)
            {
                if (propertyValuePair.Key == "Assignees")
                {
                    task.Users.Clear();
                    var userIds = propertyValuePair.Value as IEnumerable<long>;
                    if (userIds != null)
                    {
                        foreach (var userId in userIds)
                        {
                            var user = _session.Get<User>(userId);
                            if (user == null)
                            {
                                throw new ChildObjectNotFoundException("User not found");
                            }

                            task.Users.Add(user);
                        }       
                    }
                }
                else
                {
                    propertyInfos.Single(x => x.Name == propertyValuePair.Key).SetValue(task, propertyValuePair.Value);
                }
            }

            _session.SaveOrUpdate(task);

            return task;
        }
    }
}