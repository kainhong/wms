using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WMS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            List<Item1> item1 = new List<Item1>();
            List<Item2> item2 = new List<Item2>();
            var count = item1.Count(c => item2.Any(c2 => c2.Id2 == c.Id1));
        }


    }

    class Item1
    {
        public int Id1 { get; set; }
    }

    class Item2
    {
        public int Id2 { get; set; }
    }
        
}