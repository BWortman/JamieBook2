// PriorityMap.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.Mapping
{
    public class PriorityMap : VersionedClassMap<Priority>
    {
        public PriorityMap()
        {
            Id(x => x.PriorityId);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Ordinal).Not.Nullable();
        }
    }
}