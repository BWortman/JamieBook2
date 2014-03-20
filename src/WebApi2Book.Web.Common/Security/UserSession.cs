// UserSession.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Security.Claims;
using System.Web;
using WebApi2Book.Common;

namespace WebApi2Book.Web.Common.Security
{
    public class UserSession : IWebUserSession
    {
        private string _apiVersionInUse;

        public UserSession(ClaimsPrincipal principal)
        {
            // TODO
            UserId = 1; //Guid.Parse(principal.FindFirst(ClaimTypes.Sid).Value);
            //Firstname = principal.FindFirst(ClaimTypes.GivenName).Value;
            //Lastname = principal.FindFirst(ClaimTypes.Surname).Value;
            //Username = principal.FindFirst(ClaimTypes.Name).Value;
            //Email = principal.FindFirst(ClaimTypes.Email).Value;
        }

        public long UserId { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }

        public Uri RequestUri
        {
            get { return HttpContext.Current.Request.Url; }
        }

        public string HttpRequestMethod
        {
            get { return HttpContext.Current.Request.HttpMethod; }
        }

        public string ApiVersionInUse
        {
            get
            {
                if (_apiVersionInUse == null)
                {
                    var apiVersionInRequest =
                        (string) HttpContext.Current.Request.RequestContext.RouteData.Values[
                            Constants.CommonRoutingDefinitions.ApiVersionSegmentName];
                    _apiVersionInUse = string.IsNullOrWhiteSpace(apiVersionInRequest)
                        ? Constants.CommonRoutingDefinitions.CurrentApiVersion
                        : apiVersionInRequest;
                }
                return _apiVersionInUse;
            }
        }
    }
}