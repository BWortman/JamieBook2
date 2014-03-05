// CategoryByIdInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class CategoryByIdInquiryProcessor : ICategoryByIdInquiryProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ICategoryByIdQueryProcessor _queryProcessor;

        public CategoryByIdInquiryProcessor(ICategoryByIdQueryProcessor queryProcessor, IAutoMapper autoMapper)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
        }

        public Category GetCategory(long categoryId)
        {
            var category = _queryProcessor.GetCategory(categoryId);
            if (category == null)
            {
                throw new RootObjectNotFoundException("Category not found");
            }

            // TODO - add link service

            var categoryModel = _autoMapper.Map<Category>(category);
            return categoryModel;
        }
    }
}