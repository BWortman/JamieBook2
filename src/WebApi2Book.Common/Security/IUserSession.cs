// IUserSession.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;

namespace WebApi2Book.Common.Security
{
    public interface IUserSession
    {
        long UserId { get; }
        string Firstname { get; }
        string Lastname { get; }
        string Username { get; }
        string Email { get; }
        string ApiVersionInUse { get; }
        Uri RequestingUri { get; }
    }
}