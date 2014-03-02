// ICurrentSessionContextAdapter.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;

namespace WebApi2Book.Web.Common
{
    public interface ICurrentSessionContextAdapter
    {
        bool HasBind(ISessionFactory sessionFactory);
        ISession Unbind(ISessionFactory sessionFactory);
    }
}