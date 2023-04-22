using log4net;
using System;
using System.Web.Mvc;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class ForecastWeatherController : Controller
    {
        public ActionResult Index()
        {
            return View("WeatherForecastMaster",new LiveDataModel());
        }

        public ActionResult GetHistoricalData()
        {
            ViewBag.Message = "Weather Forcast Details.";
            HistoricalDataModel historicalDataModel = new HistoricalDataModel();          
            return View("WeatherForecastHistory", historicalDataModel.GetHistoricalDetails());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Weather Forcast Details.";
            return View();
        }
        [HttpPost]
        public ActionResult GetDailyWeather(WeatherForecast.Response.InterfaceModels.WeatherForecastInput weatherForecastInput)
        {
            WeatherForecastModel weatherForecastResponse = new WeatherForecastModel();
       
            try
            {
                if (weatherForecastInput != null)
                {
                    weatherForecastResponse = WeatherForecastRequestModel.GetWeatherForcastLiveData(weatherForecastInput);
                   
                }

            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(ForecastWeatherController)).Error(ex);
            }
            return PartialView("_WeatherForecastDetails", weatherForecastResponse);
        }


        [HttpPost]
        public ActionResult GetHistoryDetails(WeatherForecast.Response.InterfaceModels.WeatherForecastInput weatherForecastInput)
        {
            try
            {
                HistoricalDataModel historicalDataModel = new HistoricalDataModel();
                return Json(historicalDataModel.GetHistoricalDetailsData(weatherForecastInput),JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(ForecastWeatherController)).Error(ex);
            }
            return null;
        }

        [HttpPost]
        public ActionResult GetDetailsGrid(WeatherForecast.Response.InterfaceModels.WeatherForecastInput weatherForecastInput)
        {
            try
            {
                HistoricalDataModel historicalDataModel = new HistoricalDataModel();
                return PartialView("_DetailsGrid", historicalDataModel.GetHistoricalDetailsData(weatherForecastInput,false));
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(ForecastWeatherController)).Error(ex);
            }
            return null;
        }
        

        [HttpPost]
        public ActionResult DeleteMasterDetails(int masterId)
        {
            try
            {
                HistoricalDataModel historicalDataModel = new HistoricalDataModel();
                if (historicalDataModel.DeleteMasterDetails(masterId))
                {
                    return Json(historicalDataModel.GetHistoricalDetails(), JsonRequestBehavior.AllowGet);
                }          
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(ForecastWeatherController)).Error(ex);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRefreshedData()
        {
            ViewBag.Message = "Weather Forcast Details.";
            HistoricalDataModel historicalDataModel = new HistoricalDataModel();
            return PartialView("_HistoryPartial", historicalDataModel.GetHistoricalDetails());
        }


    }
}