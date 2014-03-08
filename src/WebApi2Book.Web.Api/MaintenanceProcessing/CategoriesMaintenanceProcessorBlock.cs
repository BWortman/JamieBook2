// CategoriesMaintenanceProcessorBlock.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class CategoriesMaintenanceProcessorBlock : ICategoriesMaintenanceProcessorBlock
    {
        public CategoriesMaintenanceProcessorBlock(ICategoryAddingProcessor categoryAddingProcessor)
        {
            CategoryAddingProcessor = categoryAddingProcessor;
        }

        public ICategoryAddingProcessor CategoryAddingProcessor { get; private set; }
    }
}