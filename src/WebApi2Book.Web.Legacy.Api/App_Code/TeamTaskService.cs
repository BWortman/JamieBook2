// TeamTaskService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Services;
using WebApi2Book.Web.Api.Models;

/// <summary>
///     Summary description for TeamTaskService
/// </summary>
/// <remarks>
///     This would obviously normally fetch data from the database, but we're keeping things very
///     simple to maintain focus on the Web API, not on legacy web services or data access technologies.
/// </remarks>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class TeamTaskService : WebService
{
    [WebMethod]
    public Status[] GetStatuses()
    {
        return new[]
        {
            new Status {StatusId = 1, Name = "status1", Ordinal = 1},
            new Status {StatusId = 2, Name = "status2", Ordinal = 2}
        };
    }

    [WebMethod]
    public Status GetStatusById(int statusId)
    {
        return new Status {StatusId = statusId, Name = "status" + statusId, Ordinal = statusId};
    }
}