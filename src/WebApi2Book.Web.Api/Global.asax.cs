// Global.asax.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web;
using System.Web.Http;
using WebApi2Book.Web.Legacy;

namespace WebApi2Book.Web.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);


            var legacyFormatter = new LegacyMessageTypeFormatter();
            GlobalConfiguration.Configuration.Formatters.Insert(0, legacyFormatter);
        }
    }
}