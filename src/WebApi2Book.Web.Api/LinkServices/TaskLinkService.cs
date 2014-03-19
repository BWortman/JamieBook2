// TaskLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common;
using WebApi2Book.Common.Security;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class TaskLinkService : ITaskLinkService
    {
        private readonly IUserSession _userSession;

        public TaskLinkService(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public void AddLinks(Task task)
        {
            AddSelfLink(task);
            AddAllTasksLink(task);
        }

        public virtual void AddSelfLink(Task task)
        {
            task.AddLink(new Link
            {
                Title = Constants.CommonLinkTitles.Self,
                Rel = Constants.CommonLinkRelValues.Self,
                Href = string.Format("/api/{0}/tasks/{1}", _userSession.ApiVersionInUse, task.TaskId)
            });
        }

        public virtual void AddAllTasksLink(Task task)
        {
            task.AddLink(new Link
            {
                Title = "All Tasks",
                Rel = Constants.CommonLinkRelValues.All,
                Href = string.Format("/api/{0}/tasks", _userSession.ApiVersionInUse)
            });
        }
    }
}