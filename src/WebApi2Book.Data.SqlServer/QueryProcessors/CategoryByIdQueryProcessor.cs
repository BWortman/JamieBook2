// CategoryByIdQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class CategoryByIdQueryProcessor : ICategoryByIdQueryProcessor
    {
        private readonly ISession _session;

        public CategoryByIdQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Category GetCategory(long categoryId)
        {
            var category = _session.Get<Category>(categoryId);
            return category;
        }
    }
}