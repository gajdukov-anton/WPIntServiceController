using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing.Constraints;

namespace WPIntServiceController
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

           
            config.Routes.MapHttpRoute(
                name: "ActionRoute",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { },
                constraints: new
                {
                    action = new AlphaRouteConstraint()
                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { },
                constraints: new
                {
                    id = new IntRouteConstraint()
                }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
