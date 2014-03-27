// UpdateTaskMaintenanceProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    /// <summary>
    ///     Updates the specified Task.
    /// </summary>
    /// <remarks>
    ///     This implementation only supports Json. Support for other content types is
    ///     left as an exercise for the reader :-)
    /// </remarks>
    public class UpdateTaskMaintenanceProcessor : IUpdateTaskMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IUpdateTaskQueryProcessor _queryProcessor;
        private readonly ITaskLinkService _taskLinkService;
        private readonly IUpdateablePropertyDetector _updateablePropertyDetector;

        public UpdateTaskMaintenanceProcessor(IUpdateTaskQueryProcessor queryProcessor, IAutoMapper autoMapper,
            ITaskLinkService taskLinkService, IUpdateablePropertyDetector updateablePropertyDetector)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskLinkService = taskLinkService;
            _updateablePropertyDetector = updateablePropertyDetector;
        }

        public Task UpdateTask(object taskFragment)
        {
            var taskWithUpdates = ((JObject) taskFragment).ToObject<Task>();

            var propertiesToUpdate = _updateablePropertyDetector.GetNamesOfPropertiesToUpdate(typeof (Task),
                taskFragment).ToList();

            var propertyInfos = typeof (Task).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var updatedPropertyValueMap = new Dictionary<string, object>();
            foreach (var propertyName in propertiesToUpdate)
            {
                var propertyValue = propertyInfos.Single(x => x.Name == propertyName).GetValue(taskWithUpdates);

                if (propertyName == "Assignees")
                {
                    var users = propertyValue as IEnumerable<User>;
                    var valueToAdd = users == null ? null : users.Select(x => x.UserId);
                    updatedPropertyValueMap.Add(propertyName, valueToAdd);
                }
                else
                {
                    updatedPropertyValueMap.Add(propertyName, propertyValue);
                }
            }

            var updatedTaskEntity = _queryProcessor.GetUpdatedTask(taskWithUpdates.TaskId, updatedPropertyValueMap);

            var task = _autoMapper.Map<Task>(updatedTaskEntity);

            _taskLinkService.AddLinks(task);

            return task;
        }
    }
}