// CurrentSessionContextAdapter.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using NHibernate.Context;

namespace WebApi2Book.Web.Common
{
    public class CurrentSessionContextAdapter : ICurrentSessionContextAdapter
    {
        public bool HasBind(ISessionFactory sessionFactory)
        {
            return CurrentSessionContext.HasBind(sessionFactory);
        }

        public ISession Unbind(ISessionFactory sessionFactory)
        {
            return CurrentSessionContext.Unbind(sessionFactory);
        }
    }
}