using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Web;
using System.Collections.Specialized;
using System.Configuration;

namespace InterviewTask.Controllers
{
    public class WeatherController : ApiController {

        private static readonly HttpClient client = new HttpClient();
        private static readonly string ApiKey = ConfigurationManager.AppSettings["OpenWeatherApiKey"];
        public HttpResponseMessage Get([FromUri] string Lat, string Lon) {

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("lat", Lat);
            queryString.Add("lon", Lon);
            queryString.Add("appid", ApiKey);
            var weatherResponse = client.GetAsync(string.Format("https://api.openweathermap.org/data/2.5/weather?{0}", queryString.ToString()));
            return weatherResponse.Result;
        }
    }
}
