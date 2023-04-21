using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Response.InterfaceModels
{
    public class WeatherForecastResponse
    {
        public OpenMeteo.WeatherForecast weatherForecast { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
