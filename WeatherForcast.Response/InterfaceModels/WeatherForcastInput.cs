using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForcast.Response.InterfaceModels
{
    public class WeatherForcastInput
    {
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
    }
}
