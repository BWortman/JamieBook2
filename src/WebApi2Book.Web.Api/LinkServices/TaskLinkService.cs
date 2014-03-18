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

        public void AddLinks(Task modelTask)
        {
            AddSelfLink(modelTask);
            AddAllTasksLink(modelTask);
        }

        public virtual void AddSelfLink(Task modelTask)
        {
            modelTask.AddLink(new Link
            {
                Title = Constants.CommonLinkTitles.Self,
                Rel = Constants.CommonLinkRelValues.Self,
                Href = string.Format("/api/{0}/tasks/{1}", _userSession.ApiVersionInUse, modelTask.TaskId)
            });
        }

        public virtual void AddAllTasksLink(Task modelTask)
        {
            modelTask.AddLink(new Link
            {
                Title = "All Tasks",
                Rel = Constants.CommonLinkRelValues.All,
                Href = string.Format("/api/{0}/tasks", _userSession.ApiVersionInUse)
            });
        }
    }
}