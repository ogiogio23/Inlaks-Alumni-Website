using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InlaksAlumniWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DeleteEvent",
                url: "deleteevent/{id}",
                defaults: new { controller = "Admin", action = "DeleteEvent" }
            );

            routes.MapRoute(
                name: "EditEvent",
                url: "editevent/{id}",
                defaults: new { controller = "Admin", action = "EditEvent" }
            );

            routes.MapRoute(
                name: "DeleteAdmin",
                url: "deleteadmin/{id}",
                defaults: new { controller = "Admin", action = "DeleteAdmin" }
            );

            routes.MapRoute(
                name: "EditAdmin",
                url: "editadmin/{id}",
                defaults: new { controller = "Admin", action = "EditAdmin" }
            );



            routes.MapRoute(
                name: "DeleteAlumni",
                url: "deletealumni/{id}",
                defaults: new { controller = "Admin", action = "DeleteAlumni" }
            );


            routes.MapRoute(
                name: "Edit",
                url: "edit/{id}",
                defaults: new { controller = "Admin", action = "Edit" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
