using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class ForecastWeatherController : Controller
    {
        public ActionResult Index()
        {
            return View("WeatherForecastMaster");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult GetDailyWeather(WeatherForecast.Response.InterfaceModels.WeatherForecastInput weatherForecastInput)
        {
            WeatherForecastModel weatherForecastResponse = new WeatherForecastModel();
            if (weatherForecastInput!=null)
            {
                weatherForecastResponse = WeatherForecastRequestModel.GetWeatherForcastLiveData(weatherForecastInput);
            }

            return View("WeatherForecastMaster", weatherForecastResponse);
        }
    }
}