using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;

namespace SOFTTEK.SCMS.SRA
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
            config.Routes.MapHttpRoute(
                name: "ApiById",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, action = "DefaultAction" }
            );

            config.Routes.MapHttpRoute(
                name: "ApiByIdWithAction",
                routeTemplate: "api/{controller}/{id}/{action}/",
                defaults: new { id = RouteParameter.Optional }
            );


            //config.MapHttpAttributeRoutes();

            

            

            
        }
    }
}
