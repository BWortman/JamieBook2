// TeamTaskService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
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
        return new[]
        {
            new Category {Name = "cat1", Description = "category 1"},
            new Category {Name = "cat2", Description = "category 2"}
        };
    }

    [WebMethod]
    public Status[] GetStatuses()
    {
        return new[]
        {
            new Status() {Name = "status1"},
            new Status() {Name = "status2"}
        };
    }
}