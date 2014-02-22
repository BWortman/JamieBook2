// WebApiConfig.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http;

namespace WebApi2Book.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "legacy",
                routeTemplate: "TeamTaskService/TeamTaskService.asmx",
                defaults:
                    new
                    {
                        controller = "Legacy"
                    }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
        }
    }
}