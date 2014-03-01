// Priority.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Data.Entities
{
    public class Priority : IVersionedEntity
    {
        public virtual long PriorityId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Ordinal { get; set; }
        public virtual byte[] Version { get; set; }
    }
}