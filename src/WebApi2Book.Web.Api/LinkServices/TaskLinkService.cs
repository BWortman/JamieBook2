// TaskLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common.Security;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class TaskLinkService : ITaskLinkService
    {
        private readonly ICommonLinkService _commonLinkService;
        private readonly IWebUserSession _userSession;

        public TaskLinkService(IWebUserSession userSession, ICommonLinkService commonLinkService)
        {
            _userSession = userSession;
            _commonLinkService = commonLinkService;
        }

        public void AddLinks(Task task)
        {
            AddSelfLink(task);
            AddAllTasksLink(task);
        }

        public virtual void AddSelfLink(Task task)
        {
            task.AddLink(GetSelfLink(task));
        }

        public virtual Link GetAllTasksLink()
        {
            var path =
                string.Format(
                    Constants.CommonRoutingDefinitions.DelimitedVersionedApiRouteBaseFormatString + "tasks",
                    _userSession.ApiVersionInUse);
            return _commonLinkService.GetLink(path, Constants.CommonLinkRelValues.All, HttpMethod.Get);
        }

        public virtual void AddAllTasksLink(Task task)
        {
            task.AddLink(GetAllTasksLink());
        }

        public virtual Link GetSelfLink(Task task)
        {
            var path =
                string.Format(
                    Constants.CommonRoutingDefinitions.DelimitedVersionedApiRouteBaseFormatString + "tasks/{1}",
                    _userSession.ApiVersionInUse, task.TaskId);
            var link = _commonLinkService.GetLink(path, Constants.CommonLinkRelValues.Self, HttpMethod.Get);
            return link;
        }
    }
}