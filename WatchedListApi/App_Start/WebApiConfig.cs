using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using IncleverApi.Converters;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace IncleverApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("*", "*", "POST,OPTIONS,GET"));                      

            registrarFormatters(config);
            // Configuración y servicios de API web            
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // handler de seguridad
            config.MessageHandlers.Add(new Handlers.ApiKeyHandler("clave", "DEV_KEY"));
            //query string handler
            //config.MessageHandlers.Add(new Handlers.QueryStringHandler());

            //config.Filters.Add(new CustomAuAttribute());
            // model binders especificos
            config.Services.Insert(typeof(System.Web.Http.ModelBinding.ModelBinderProvider), 0, new Binders.FakeItemModelBinder());

            //model binder generico
            //config.Services.Insert(typeof(System.Web.Http.ModelBinding.ModelBinderProvider), 1, new Binders.GenericModelBinder());

        }
        static void registrarFormatters(HttpConfiguration config)
        {

            ///FUCIONA
            /// config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            ///FUNCIONA
            // GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));
            //GlobalConfiguration.Configuration.Formatters.Add();

            ///para usar cualquiera de estas comentar en GlobalAsax 
            /// var config = GlobalConfiguration.Configuration;
            ///GlobalConfiguration.Configuration.AddJsonpFormatter(config.Formatters.JsonFormatter, "callback");

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            //foreach (var formatter in GlobalConfiguration.Configuration.Formatters)
            //{
            //    var jsonFormatter = formatter as JsonMediaTypeFormatter;
            //    if (jsonFormatter == null)
            //        continue;

            //    jsonFormatter.SerializerSettings.Converters.Add(new CustomConverter());
            //}
        }
    }

}
