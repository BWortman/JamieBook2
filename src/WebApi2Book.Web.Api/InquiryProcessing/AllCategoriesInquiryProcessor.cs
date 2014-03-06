// AllCategoriesInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class AllCategoriesInquiryProcessor : IAllCategoriesInquiryProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ICategoryLinkService _categoryLinkService;
        private readonly IAllCategoriesQueryProcessor _queryProcessor;

        public AllCategoriesInquiryProcessor(IAllCategoriesQueryProcessor queryProcessor, IAutoMapper autoMapper,
            ICategoryLinkService categoryLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _categoryLinkService = categoryLinkService;
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _queryProcessor.GetCategories();

            var modelCategories = categories.Select(x => _autoMapper.Map<Category>(x)).ToList();

            modelCategories.ForEach(x => _categoryLinkService.AddLinks(x));

            return modelCategories;
        }
    }
}