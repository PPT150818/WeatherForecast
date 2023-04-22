using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Principal;
using System.Web.Http;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class APIForecastWeatherController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetWeatherForecastDetails(DataSourceLoadOptions loadOptions, int? weatherForcastId)
        {
            var data = new DbPersist.Entities.WeatherForecastMaster();
            try
            {
               // data = DataSourceLoader.Load(data, loadOptions);
                

            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(APIForecastWeatherController)).Error(ex);
            }
            return Request.CreateResponse(data);
        }

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            var data = new DbPersist.Entities.WeatherForecastMaster();
            
            try
            {
                //List <WeatherForecastModel> weatherForecastModels = WeatherForecastModel.GetWeatherForecastMasterModel();
                //return Request.CreateResponse(weatherForecastModels);

            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(APIForecastWeatherController)).Error(ex);
            }
            return Request.CreateResponse(data);
        }

        [HttpDelete]
        public void DeleteWeatherForecastMaster(FormDataCollection form)
        {
            try
            {
                var key = form.Get("key");
                var values = form.Get("values");

                int id = Convert.ToInt32(key);

            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(APIForecastWeatherController)).Error(ex);
            }
        }

        [HttpDelete]
        public void DeleteWeatherForecastDetails(FormDataCollection form)
        {
            try
            {
                var key = form.Get("key");
                var values = form.Get("values");

                int id = Convert.ToInt32(key);

            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(APIForecastWeatherController)).Error(ex);
            }
        }
    }
}
