// tasksMaintenanceProcessorBlock.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class TasksMaintenanceProcessorBlock : ITasksMaintenanceProcessorBlock
    {
        public TasksMaintenanceProcessorBlock(ITaskAddingProcessor taskAddingProcessor)
        {
            TaskAddingProcessor = taskAddingProcessor;
        }

        public ITaskAddingProcessor TaskAddingProcessor { get; private set; }
    }
}