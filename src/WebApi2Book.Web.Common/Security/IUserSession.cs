// IUserSession.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;

namespace WebApi2Book.Web.Common.Security
{
    public interface IUserSession
    {
        Guid UserId { get; }
        string Firstname { get; }
        string Lastname { get; }
        string Username { get; }
        string Email { get; }
        string ApiVersionInUse { get; }
        Uri RequestingUri { get; }
    }
}