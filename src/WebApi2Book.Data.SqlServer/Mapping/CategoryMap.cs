// CategoryMap.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.Mapping
{
    public class CategoryMap : VersionedClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.CategoryId);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Description).Nullable();
        }
    }
}