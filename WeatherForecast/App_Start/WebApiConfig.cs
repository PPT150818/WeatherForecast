﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WeatherForecast
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                  routeTemplate: "api/{controller}/{action}",
                    defaults: new { action = "Get", id = RouteParameter.Optional }
            );
        }
    }
}
