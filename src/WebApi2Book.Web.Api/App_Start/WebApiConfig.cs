// WebApiConfig.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Tracing;
using WebApi2Book.Common.Logging;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.ErrorHandling;

namespace WebApi2Book.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ConfigureRouting(config);

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
            config.Services.Replace(typeof(ITraceWriter),
                new SimpleTraceWriter(WebContainerManager.Get<ILogManager>()));

            config.Services.Add(typeof(IExceptionLogger),
                new SimpleExceptionLogger(WebContainerManager.Get<ILogManager>()));

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }

        private static void ConfigureRouting(HttpConfiguration config)
        {
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