// Category.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Data.Entities
{
    public class Category : IVersionedEntity
    {
        public virtual long CategoryId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual byte[] Version { get; set; }
    }
}