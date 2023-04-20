using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeatherForcast.Response.InterfaceModels;
using WeatherForecast.API.Models;

namespace WeatherForecast.API.Controllers
{
    public class WeatherForcastController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // GET api/values
        [HttpGet]
        public WeatherForcastResponse GetWeatherForcastDetails([FromBody] WeatherForcastInput weatherForcastInput)
        {
            WeatherForcastResponse weatherForcastResponse = new WeatherForcastResponse();
            if (weatherForcastInput == null || (weatherForcastInput != null && (weatherForcastInput.Latitude == null || weatherForcastInput.Latitude == null)))
            {
                weatherForcastResponse.HttpStatusCode = HttpStatusCode.BadRequest;
                return weatherForcastResponse;
            }

            try
            {
                OpenMeteoClient client = new OpenMeteoClient();
                weatherForcastResponse.weatherForecast = client.QueryWeatherForecast(weatherForcastInput.Latitude.Value, weatherForcastInput.Longitude.Value);
                weatherForcastResponse.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                weatherForcastResponse.HttpStatusCode = HttpStatusCode.InternalServerError;
                log4net.LogManager.GetLogger(typeof(WeatherForcastController)).Error(ex);
            }

            return weatherForcastResponse;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
