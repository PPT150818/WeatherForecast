using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeatherForecast.Response.InterfaceModels;
using WeatherForecast.API.Models;

namespace WeatherForecast.API.Controllers
{
    public class WeatherForcastController : ApiController
    {
        // GET api/values
        /// <summary>
        /// This API method returns
        /// </summary>
        /// <param name="weatherForcastInput">Latitude and Longitude provided by user</param>
        /// <returns></returns>
        [HttpPost]
        public WeatherForecastResponse GetWeatherForcastDetails([FromBody] WeatherForecastInput weatherForcastInput)
        {
            WeatherForecastResponse weatherForcastResponse = new WeatherForecastResponse();
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
    }
}
