// IAllUsersQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public interface IAllUsersQueryProcessor
    {
        QueryResult<User> GetUsers(PagedDataRequest requestInfo);
    }
}