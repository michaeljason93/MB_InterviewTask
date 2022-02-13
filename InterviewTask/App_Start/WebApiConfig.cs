using System.Net.Http.Headers;
using System.Web.Http;

namespace InterviewTask.App_Start {
    public class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html")); 
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}"
            );
        }
    }
}