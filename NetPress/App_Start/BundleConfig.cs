using System.Web;
using System.Web.Optimization;

namespace NetPress
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/Admin/jquery").Include(
                "~/Scripts/Admin/bootstrap.js",
                "~/Scripts/Admin/bootstrap.min.js",
                "~/Scripts/Admin/jquery.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Admin/css").Include(
                   "~/Content/Admin/bootstrap.css",
                   "~/Content/Admin/bootstrap.min.css",
                   "~/Content/Admin/css/font-awesome*",
                   "~/Content/Admin/sb-admin.css",
                    "~/Content/Admin/fonts/fontawesome-*",
                    "~/Content/Admin/fonts/Fontawesome.otf",
                    "~/Content/Admin/Site.css"

                    ));

            bundles.Add(new StyleBundle("~/Content/Admin/less").
                IncludeDirectory("~/Content/Admin/less/", "*.less", true));

            bundles.Add(new StyleBundle("~/Content/Admin/scss").
                IncludeDirectory("~/Content/Admin/scss/", "*.scss", true));

            bundles.Add(new StyleBundle("~/Content/Home/css").Include(
                     "~/Content/Home/css/bootstrap.css",
                     "~/Content/Home/css/bootstrap.min.css",
                   "~/Content/Home/css/blog-home.css"

                      ));
        }
    }
}
