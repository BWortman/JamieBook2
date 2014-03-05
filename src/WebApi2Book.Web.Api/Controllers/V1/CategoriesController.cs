// CategoriesController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NHibernate;
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
        private readonly ISession _session;
        private readonly ICategoryByIdInquiryProcessor _categoryByIdInquiryProcessor;

        public CategoriesController(ISession session, ICategoryByIdInquiryProcessor categoryByIdInquiryProcessor)
        {
            _session = session;
            _categoryByIdInquiryProcessor = categoryByIdInquiryProcessor;
        }

        [Route("", Name = "GetCategoriesRoute")]
        public IEnumerable<Category> GetCategories()
        {
            return _session
                .QueryOver<Data.Entities.Category>()
                .List()
                .Select(x => new Category {CategoryId = x.CategoryId, Name = x.Name, Description = x.Description})
                .ToList();
        }

        [Route("{id:long}", Name = "GetCategoryRoute")]
        public Category Get(long id)
        {
            var category = _categoryByIdInquiryProcessor.GetCategory(id);
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