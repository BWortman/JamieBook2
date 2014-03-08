// CategoryAddingProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class CategoryAddingProcessor : ICategoryAddingProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ICategoryLinkService _categoryLinkService;
        private readonly ISession _session;

        public CategoryAddingProcessor(ISession session, IAutoMapper autoMapper, ICategoryLinkService categoryLinkService)
        {
            _session = session;
            _autoMapper = autoMapper;
            _categoryLinkService = categoryLinkService;
        }

        public Category AddCategory(Category modelCategory)
        {
            var category = _autoMapper.Map<Data.Entities.Category>(modelCategory);
            _session.Save(category);

            var newModelCategory = _autoMapper.Map<Category>(category);
            _categoryLinkService.AddLinks(newModelCategory);

            return newModelCategory;
        }
    }
}