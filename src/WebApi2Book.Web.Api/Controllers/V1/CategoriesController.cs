// CategoriesController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Web.Http;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("categories")]
    public class CategoriesController : ApiController
    {
        [Route("", Name = "GetCategoriesRoute")]
        public IEnumerable<Category> GetCategories()
        {
            return new[] {new Category {CategoryId = 1, Name = "cat1"}, new Category {CategoryId = 2, Name = "cat2"}};
            //return _session
            //    .QueryOver<Data.Model.Category>()
            //    .List()
            //    .Select(_categoryMapper.CreateCategory)
            //    .ToList();
        }

        [Route("{id:long}", Name = "GetCategoryRoute")]
        public Category Get(long id)
        {
            return new Category {CategoryId = id, Name = "cat" + id};

            //var category = _categoryFetcher.GetCategory(id);
            //return _categoryMapper.CreateCategory(category);
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