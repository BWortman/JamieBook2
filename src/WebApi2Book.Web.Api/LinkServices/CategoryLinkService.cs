// CategoryLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class CategoryLinkService : ICategoryLinkService
    {
        // TODO: API version

        public void AddLinks(Category modelCategory)
        {
            AddSelfLink(modelCategory);
            AddAllCategoriesLink(modelCategory);
        }

        public virtual void AddSelfLink(Category modelCategory)
        {
            modelCategory.Links.Add(new Link
            {
                Title = "self",
                Rel = "self",
                Href = "/api/categories/" + modelCategory.CategoryId
            });
        }

        public virtual void AddAllCategoriesLink(Category modelCategory)
        {
            modelCategory.Links.Add(new Link
            {
                Title = "All Categories",
                Rel = "all",
                Href = "/api/categories"
            });
        }
    }
}