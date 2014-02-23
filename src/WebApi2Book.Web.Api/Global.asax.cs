// Global.asax.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
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
        }

        private void ConfigureFormatters()
        {
            var legacyFormatter = (MediaTypeFormatter) WebContainerManager.Get<ILegacyMessageTypeFormatter>();
            GlobalConfiguration.Configuration.Formatters.Insert(0, legacyFormatter);
        }

        private void RegisterHandlers()
        {
        }
    }
}