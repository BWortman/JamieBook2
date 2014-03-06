// CategoriesController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Web.Http;
using WebApi2Book.Web.Api.InquiryProcessing;
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

        public CategoriesController(ICategoriesInquiryProcessorBlock categoriesInquiryProcessorBlock)
        {
            _categoriesInquiryProcessorBlock = categoriesInquiryProcessorBlock;
        }

        [Route("", Name = "GetCategoriesRoute")]
        public IEnumerable<Category> GetCategories()
        {
            var categories = _categoriesInquiryProcessorBlock.AllCategoriesInquiryProcessor.GetCategories();
            return categories;
        }

        [Route("{id:long}", Name = "GetCategoryRoute")]
        public Category Get(long id)
        {
            var category = _categoriesInquiryProcessorBlock.CategoryByIdInquiryProcessor.GetCategory(id);
            return category;
        }

        //[AdministratorAuthorized]
        //public HttpResponseMessage Post(HttpRequestMessage request, Category category)
        //{
        //    var modelCategory = new Data.Model.Category
        //                            {
        //                                Description = category.Description,
        //                                Name = category.Name
        //                            };

        //    _session.Save(modelCategory);

        //    var newCategory = _categoryMapper.CreateCategory(modelCategory);

        //    var href = newCategory.Links.First(x => x.Rel == "self").Href;

        //    var response = request.CreateResponse(HttpStatusCode.Created, newCategory);
        //    response.Headers.Add("Location", href);

        //    return response;
        //}

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