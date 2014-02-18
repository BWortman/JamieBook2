// User.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Link> Links { get; set; }
    }
}