// IUserSession.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Common.Security
{
    /// <summary>
    ///     Provides access to information pertaining to the current principal.
    /// </summary>
    public interface IUserSession
    {
        long UserId { get; }
        string Firstname { get; }
        string Lastname { get; }
        string Username { get; }
        string Email { get; }
    }
}