// UserSession.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Security.Claims;
using System.Web;
using WebApi2Book.Common;

namespace WebApi2Book.Web.Common.Security
{
    public class UserSession : IUserSession
    {
        public UserSession(ClaimsPrincipal principal, Uri requestingUri)
        {
            // TODO
            //UserId = Guid.Parse(principal.FindFirst(ClaimTypes.Sid).Value);
            //Firstname = principal.FindFirst(ClaimTypes.GivenName).Value;
            //Lastname = principal.FindFirst(ClaimTypes.Surname).Value;
            //Username = principal.FindFirst(ClaimTypes.Name).Value;
            //Email = principal.FindFirst(ClaimTypes.Email).Value;
            RequestingUri = requestingUri;
        }

        public Guid UserId { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }

        public Uri RequestingUri { get; private set; }

        public string ApiVersionInUse
        {
            get
            {
                var apiVersionInRequest =
                    (string) HttpContext.Current.Request.RequestContext.RouteData.Values[
                        Constants.CommonRoutingDefinitions.ApiVersionSegmentName];
                var apiVersion = string.IsNullOrWhiteSpace(apiVersionInRequest)
                    ? Constants.CommonRoutingDefinitions.CurrentApiVersion
                    : apiVersionInRequest;
                return apiVersion;
            }
        }
    }
}