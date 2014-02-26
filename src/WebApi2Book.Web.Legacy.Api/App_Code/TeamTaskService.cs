// TeamTaskService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Services;
using WebApi2Book.Web.Api.Models;

/// <summary>
///     Summary description for TeamTaskService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class TeamTaskService : WebService
{
    public TeamTaskService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public Category[] GetCategories()
    {
        // todo: fetch from db
        return new[]
        {
            new Category {CategoryId = 1, Name = "cat1", Description = "category 1"},
            new Category {CategoryId = 2, Name = "cat2", Description = "category 2"}
        };
    }

    [WebMethod]
    public Category GetCategoryById(int categoryId)
    {
        // todo: fetch from db
        return new Category {CategoryId = 1, Name = "cat1", Description = "category 1"};
    }
}