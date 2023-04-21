using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Response.InterfaceModels
{
    public class WeatherForecastInput
    {
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
    }
}
