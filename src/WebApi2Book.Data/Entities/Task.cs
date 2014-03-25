// Task.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;

namespace WebApi2Book.Data.Entities
{
    public class Task : IVersionedEntity
    {
        private IList<User> _users;
        public virtual long TaskId { get; set; }
        public virtual string Subject { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual DateTime? DateCompleted { get; set; }
        public virtual Status Status { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User CreatedBy { get; set; }

        public virtual IList<User> Users
        {
            get { return _users ?? (_users = new List<User>()); }
            set { _users = value; }
        }

        public virtual byte[] Version { get; set; }
    }
}