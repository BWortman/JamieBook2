// ApiVersion1RoutePrefixAttribute.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http;

namespace WebApi2Book.Web.Common.Routing
{
    public class ApiVersion1RoutePrefixAttribute : RoutePrefixAttribute
    {
        public ApiVersion1RoutePrefixAttribute(string prefix)
            : base("api/{apiVersion:apiVersionConstraint(v1)}/" + prefix)
        {
        }
    }
}