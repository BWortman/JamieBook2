// AllUsersQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using NHibernate;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AllUsersQueryProcessor : IAllUsersQueryProcessor
    {
        private readonly ISession _session;

        public AllUsersQueryProcessor(ISession session)
        {
            _session = session;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _session.QueryOver<User>().List();
            return users;
        }
    }
}