// CategoriesController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("categories")]
    [UnitOfWorkActionFilter]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesInquiryProcessorBlock _categoriesInquiryProcessorBlock;
        private readonly ICategoriesMaintenanceProcessorBlock _categoriesMaintenanceProcessorBlock;

        public CategoriesController(ICategoriesInquiryProcessorBlock categoriesInquiryProcessorBlock,
            ICategoriesMaintenanceProcessorBlock categoriesMaintenanceProcessorBlock)
        {
            _categoriesInquiryProcessorBlock = categoriesInquiryProcessorBlock;
            _categoriesMaintenanceProcessorBlock = categoriesMaintenanceProcessorBlock;
        }

        [Route("", Name = "GetCategoriesRoute")]
        public IEnumerable<Category> GetCategories()
        {
            var modelCategories = _categoriesInquiryProcessorBlock.AllCategoriesInquiryProcessor.GetCategories();
            return modelCategories;
        }

        [Route("{id:long}", Name = "GetCategoryRoute")]
        public Category GetCategory(long id)
        {
            var modelCategory = _categoriesInquiryProcessorBlock.CategoryByIdInquiryProcessor.GetCategory(id);
            return modelCategory;
        }

        //[AdministratorAuthorized]
        [Route("", Name = "AddCategoryRoute")]
        [HttpPost]
        public IHttpActionResult Add(HttpRequestMessage request, Category category)
        {
            var newModelCategory = _categoriesMaintenanceProcessorBlock.CategoryAddingProcessor.AddCategory(category);
            return new ItemCreatedActionResult(request, newModelCategory);
        }

        //[AdministratorAuthorized]
        //public HttpResponseMessage Delete()
        //{
        //    var categories = _session.QueryOver<Data.Model.Category>().List();
        //    foreach (var category in categories)
        //    {
        //        _session.Delete(category);
        //    }

        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}

        //[AdministratorAuthorized]
        //public HttpResponseMessage Delete(long id)
        //{
        //    var category = _session.Get<Data.Model.Category>(id);
        //    if (category != null)
        //    {
        //        _session.Delete(category);
        //    }

        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}

        //[AdministratorAuthorized]
        //public Category Put(long id, Category category)
        //{
        //    var modelCateogry = _categoryFetcher.GetCategory(id);

        //    modelCateogry.Name = category.Name;
        //    modelCateogry.Description = category.Description;

        //    _session.SaveOrUpdate(modelCateogry);

        //    return _categoryMapper.CreateCategory(modelCateogry);
        //}
    }
}