using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WeatherForecast
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new RequireHttpsAttribute());
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.GivenName;
            var log4NetPath = Server.MapPath("~/Log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(log4NetPath));
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            // Use HttpContext.Current to get a Web request processing helper
            HttpServerUtility server = HttpContext.Current.Server;
            Exception exception = server.GetLastError();
            if (exception is HttpUnhandledException)
                exception = exception.InnerException;

            // Log an exception
            AddToLog(exception.Message, exception.StackTrace);
        }

        void AddToLog(string message, string stackTrace)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Application Error");
            sb.AppendLine(DateTime.Now.ToLocalTime().ToString());
            sb.AppendLine(message);
            sb.AppendLine();
            sb.AppendLine("Source File: " + HttpContext.Current.Request.RawUrl);
            sb.AppendLine();
            sb.AppendLine("Stack Trace: ");
            sb.AppendLine(stackTrace);
            for (int i = 0; i < 150; i++)
                sb.Append("-");
            sb.AppendLine();
            sb.AppendLine();

            LogManager.GetLogger("Application").Error(sb);
        }
    }
}
