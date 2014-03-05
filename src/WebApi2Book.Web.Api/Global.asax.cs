// Global.asax.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using WebApi2Book.Common;
using WebApi2Book.Common.Logging;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Legacy;

namespace WebApi2Book.Web.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ConfigureFormatters();

            RegisterHandlers();

            new AutoMapperConfigurator().Configure(WebContainerManager.GetAll<IAutoMapperTypeConfigurator>());
        }

        private void ConfigureFormatters()
        {
            var legacyFormatter = (MediaTypeFormatter) WebContainerManager.Get<ILegacyMessageTypeFormatter>();
            GlobalConfiguration.Configuration.Formatters.Insert(0, legacyFormatter);
        }

        private void RegisterHandlers()
        {
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if (exception != null)
            {
                var log = WebContainerManager.Get<ILogManager>().GetLog(typeof(WebApiApplication));
                log.Error("Unhandled exception.", exception);
            }
        }
    }
}