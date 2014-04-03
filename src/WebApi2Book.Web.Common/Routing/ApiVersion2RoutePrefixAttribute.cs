// ApiVersion2RoutePrefixAttribute.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http;

namespace WebApi2Book.Web.Common.Routing
{
    public class ApiVersion2RoutePrefixAttribute : RoutePrefixAttribute
    {
        private const string RouteBase = "api/{apiVersion:apiVersionConstraint(v2)}";
        private const string PrefixRouteBase = RouteBase + "/";

        public ApiVersion2RoutePrefixAttribute(string routePrefix)
            : base(string.IsNullOrWhiteSpace(routePrefix) ? RouteBase : PrefixRouteBase + routePrefix)
        {
        }
    }
}