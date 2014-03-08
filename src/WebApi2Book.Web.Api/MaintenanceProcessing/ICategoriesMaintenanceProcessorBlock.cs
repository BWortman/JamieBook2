// ICategoriesMaintenanceProcessorBlock.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface ICategoriesMaintenanceProcessorBlock
    {
        ICategoryAddingProcessor CategoryAddingProcessor { get; }
    }
}