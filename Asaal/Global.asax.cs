using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Threading;
using System.Globalization;
using System.Configuration;
using Asaal.Models;

namespace Asaal
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var defaultLanguge = ConfigurationManager.AppSettings["defaultLanguage"].ToString();

            Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultLanguge);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultLanguge);
        }
    }
}
