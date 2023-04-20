using System.Web;
using System.Web.Optimization;

namespace WeatherForecast
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptBundle = new ScriptBundle("~/Scripts/bundle");
            var styleBundle = new StyleBundle("~/Content/bundle");

            // jQuery
            scriptBundle
                .Include("~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.cookie.js");

            // Bootstrap
            scriptBundle
            .Include("~/Scripts/bootstrap.js")
            .Include("~/Scripts/CommonMethodHelpers/LoadingPanel.js")
            .Include("~/Scripts/CommonMethodHelpers/CommonMethodHelpers.js")
             .Include("~/Scripts/CommonMethodHelpers/PopupMessage.js");
            // Bootstrap
            styleBundle
                .Include("~/Content/bootstrap.css");

            // Custom site styles
            styleBundle
                .Include("~/Content/Site.css");

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);

            //var scriptBundle = new ScriptBundle("~/Scripts/bundle");
            //var styleBundle = new StyleBundle("~/Content/bundle");

            //// jQuery
            //scriptBundle
            //    .Include("~/Scripts/jquery-{version}.js",
            //    "~/Scripts/jquery.unobtrusive-ajax.js",
            //    "~/Scripts/jquery.validate.js",
            //    "~/Scripts/jquery.validate.unobtrusive.js",
            //    "~/Scripts/jquery.cookie.js");

            //// Bootstrap
            //scriptBundle
            //.Include("~/Scripts/bootstrap.js")
            //.Include("~/Scripts/CommonMethodHelpers/LoadingPanel.js")
            //.Include("~/Scripts/CommonMethodHelpers/PopupMessage.js")
            // .Include("~/Scripts/CommonMethodHelpers/CommonMethodHelpers.js");
            //// Bootstrap
            //styleBundle
            //    .Include("~/Content/bootstrap.css");

            //// Custom site styles
            //styleBundle
            //    .Include("~/Content/Site.css");

            //bundles.Add(scriptBundle);
            //bundles.Add(styleBundle);

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));
            //var scriptBundle = new ScriptBundle("~/Scripts/bundle");
            //// jQuery
            //scriptBundle
            //    .Include("~/Scripts/jquery-{version}.js",
            //    "~/Scripts/jquery.unobtrusive-ajax.js",
            //    "~/Scripts/jquery.validate.js",
            //    "~/Scripts/jquery.validate.unobtrusive.js",
            //    "~/Scripts/jquery.cookie.js");
            //bundles.Add(scriptBundle);
            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            //// common js of project
            //bundles.Add(new ScriptBundle("~/Scripts/CommonMethodHelpers").Include(
            //    "~/Scripts/CommonMethodHelpers/LoadingPanel.js",
            //      "~/Scripts/CommonMethodHelpers/PopupMessage.js",
            //    "~/Scripts/CommonMethodHelpers/CommonMethodHelpers.js"

            //    ));
        }
    }
}
