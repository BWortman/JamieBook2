// IAllTasksQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public interface IAllTasksQueryProcessor
    {
        IEnumerable<Task> GetTasks();
    }
}