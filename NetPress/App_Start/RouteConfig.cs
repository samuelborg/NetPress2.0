using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NetPress
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

         //   routes.MapRoute(
         //    name: "SearchCategory",
         //    url: "Posts/Search/{searchString}",
         //    defaults: new { controller = "Posts", action = "SearchCategory", id = "{searchString}" }
         //);

         //   routes.MapRoute(
         //        name: "SearchID",
         //        url: "Posts/Search/{searchID}",
         //        defaults: new { controller = "Posts", action = "SearchID", id = "{searchID}" }
         //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
