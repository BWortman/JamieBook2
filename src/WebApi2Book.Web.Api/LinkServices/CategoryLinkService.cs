// CategoryLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common.Security;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class CategoryLinkService : ICategoryLinkService
    {
        private readonly IUserSession _userSession;

        public CategoryLinkService(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public void AddLinks(Category modelCategory)
        {
            AddSelfLink(modelCategory);
            AddAllCategoriesLink(modelCategory);
        }

        public virtual void AddSelfLink(Category modelCategory)
        {
            modelCategory.Links.Add(new Link
            {
                Title = "Self",
                Rel = "self",
                Href = string.Format("/api/{0}/categories/{1}", _userSession.ApiVersionInUse, modelCategory.CategoryId)
            });
        }

        public virtual void AddAllCategoriesLink(Category modelCategory)
        {
            modelCategory.Links.Add(new Link
            {
                Title = "All Categories",
                Rel = "all",
                Href = string.Format("/api/{0}/categories", _userSession.ApiVersionInUse)
            });
        }
    }
}