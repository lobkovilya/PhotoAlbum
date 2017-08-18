using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoAlbum.Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "EditProfile",
                "{username}/profile",
                new { controller = "Profile", action = "EditProfile", username = "" }
            );

            routes.MapRoute(
                "Manage",
                "{username}/manage",
                new { controller = "Profile", action = "Manage", username = "" }
            );

            routes.MapRoute(
                "User",
                "{userFilter}",
                new { controller = "Feed", action = "Posts"/*, username = ""*/ }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Feed", action = "Posts", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Error",
                "Error/{code}",
                new {controller = "Error", action = "Index"}
            );

        }
    }
}
