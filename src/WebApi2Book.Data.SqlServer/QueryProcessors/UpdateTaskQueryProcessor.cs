// UpdateTaskQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using NHibernate;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.Exceptions;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class UpdateTaskQueryProcessor : IUpdateTaskQueryProcessor
    {
        private readonly ISession _session;

        public UpdateTaskQueryProcessor(ISession session)
        {
            _session = session;
        }

        /// <summary>
        ///     Updates the specified task.
        /// </summary>
        /// <param name="taskId">Uniquely identifies the Task to update.</param>
        /// <param name="updatedPropertyValueMap">
        ///     Associates names of updated properties to the corresponding new values. Note that the
        ///     "Assignees" property value must either be null (to remove all assignees) or an
        ///     enumerable of User Ids (type long).
        /// </param>
        /// <returns>The updated task.</returns>
        public Task GetUpdatedTask(long taskId, PropertyValueMapType updatedPropertyValueMap)
        {
            var task = GetValidTask(taskId);

            var propertyInfos = typeof (Task).GetProperties();
            foreach (var propertyValuePair in updatedPropertyValueMap)
            {
                switch (propertyValuePair.Key)
                {
                    case "Assignees":
                        var userIds = propertyValuePair.Value as IEnumerable<long>;
                        UpdateTaskUsers(task, userIds, false);
                        break;
                    default:
                        propertyInfos.Single(x => x.Name == propertyValuePair.Key)
                            .SetValue(task, propertyValuePair.Value);
                        break;
                }
            }

            _session.SaveOrUpdate(task);

            return task;
        }

        public Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds)
        {
            var task = GetValidTask(taskId);

            UpdateTaskUsers(task, userIds, false);

            _session.SaveOrUpdate(task);

            return task;
        }

        public Task DeleteTaskUsers(long taskId)
        {
            var task = GetValidTask(taskId);

            UpdateTaskUsers(task, null, false);

            _session.SaveOrUpdate(task);

            return task;
        }

        public Task AddTaskUser(long taskId, long userId)
        {
            var task = GetValidTask(taskId);

            UpdateTaskUsers(task, new[] {userId}, true);

            _session.SaveOrUpdate(task);

            return task;
        }

        public Task DeleteTaskUser(long taskId, long userId)
        {
            var task = GetValidTask(taskId);

            var user = task.Users.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                task.Users.Remove(user);
                _session.SaveOrUpdate(task);
            }

            return task;
        }

        public virtual Task GetValidTask(long taskId)
        {
            var task = _session.Get<Task>(taskId);
            if (task == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }

            return task;
        }

        public virtual User GetValidUser(long userId)
        {
            var user = _session.Get<User>(userId);
            if (user == null)
            {
                throw new ChildObjectNotFoundException("User not found");
            }

            return user;
        }

        public virtual void UpdateTaskUsers(Task task, IEnumerable<long> userIds, bool appendToExisting)
        {
            if (!appendToExisting)
            {
                task.Users.Clear();
            }

            if (userIds != null)
            {
                foreach (var user in userIds.Select(GetValidUser))
                {
                    task.Users.Add(user);
                }
            }
        }
    }
}