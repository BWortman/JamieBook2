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
        public string Firstname
        {
            get { return ((ClaimsPrincipal) HttpContext.Current.User).FindFirst(ClaimTypes.GivenName).Value; }
        }

        public string Lastname
        {
            get { return ((ClaimsPrincipal) HttpContext.Current.User).FindFirst(ClaimTypes.Surname).Value; }
        }

        public string Username
        {
            get { return ((ClaimsPrincipal) HttpContext.Current.User).FindFirst(ClaimTypes.Name).Value; }
        }

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
                var apiVersionInRequest =
                    (string) HttpContext.Current.Request.RequestContext.RouteData.Values[
                        Constants.CommonRoutingDefinitions.ApiVersionSegmentName];
                return apiVersionInRequest;
            }
        }
    }
}