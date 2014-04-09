// CustomClaimTypes.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Common.Security
{
    public static class CustomClaimTypes
    {
        public const string UserId = "http://www.webapi2book.com/identity/claims/userid";

        public const string Password = "http://www.webapi2book.com/identity/claims/password";
    }
}