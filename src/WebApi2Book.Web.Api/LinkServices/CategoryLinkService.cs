// CategoryLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common;
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
            modelCategory.AddLink(new Link
            {
                Title = Constants.CommonLinkTitles.Self,
                Rel = Constants.CommonLinkRelValues.Self,
                Href = string.Format("/api/{0}/categories/{1}", _userSession.ApiVersionInUse, modelCategory.CategoryId)
            });
        }

        public virtual void AddAllCategoriesLink(Category modelCategory)
        {
            modelCategory.AddLink(new Link
            {
                Title = "All Categories",
                Rel = Constants.CommonLinkRelValues.All,
                Href = string.Format("/api/{0}/categories", _userSession.ApiVersionInUse)
            });
        }
    }
}