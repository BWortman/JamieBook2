// ISqlCommandFactory.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Data.SqlClient;

namespace WebApi2Book.Data.SqlServer
{
    public interface ISqlCommandFactory
    {
        SqlCommand GetCommand();
    }
}