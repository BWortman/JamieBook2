// AllCategoriesQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using NHibernate;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AllCategoriesQueryProcessor : IAllCategoriesQueryProcessor
    {
        private readonly ISession _session;

        public AllCategoriesQueryProcessor(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _session.QueryOver<Category>().List();
            return categories;
        }
    }
}