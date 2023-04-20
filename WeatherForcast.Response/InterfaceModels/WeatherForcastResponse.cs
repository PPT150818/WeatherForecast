using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForcast.Response.InterfaceModels
{
    public class WeatherForcastResponse
    {
        public WeatherForcast.Response.OpenMeteo.WeatherForecast weatherForecast { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
