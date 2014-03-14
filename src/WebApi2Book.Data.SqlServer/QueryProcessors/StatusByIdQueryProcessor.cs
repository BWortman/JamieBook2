// StatusByIdQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class StatusByIdQueryProcessor : IStatusByIdQueryProcessor
    {
        private readonly ISession _session;

        public StatusByIdQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Status GetStatus(long statusId)
        {
            var status = _session.Get<Status>(statusId);
            return status;
        }
    }
}