using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherForecast.Response.InterfaceModels;
using WeatherForecast.DbPersist;
namespace WeatherForecast.Models
{

    public class WeatherForecastModel 
    {
        public int WFMasterId { get; set; }
        public int WFDetailId { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public DateTime LastUpdated { get; set; }
        public WeatherForecast.Response.OpenMeteo.WeatherForecast WeatherForecast { get; set; }

        public static List<WeatherForecastModel> GetWeatherForecastMasterModel()
        {
            List<WeatherForecastModel> weatherForecastModel = new List<WeatherForecastModel>();
            DbPersistDbContext dbPersistDbContext = new DbPersistDbContext();
            weatherForecastModel = dbPersistDbContext.WeatherForecastMasterList.Select(wfm => new WeatherForecastModel { WFMasterId = wfm.Id, Latitude = wfm.Latitude, Longitude = wfm.Longitude, LastUpdated = wfm.UpdatedDateTime }).Distinct().ToList();
            return weatherForecastModel;
        }

        public static List<WeatherForecastModel> GetWeatherForecastDetailModel(int masterId)
        {
            List<WeatherForecastModel> weatherForecastModel = new List<WeatherForecastModel>();
            DbPersistDbContext dbPersistDbContext = new DbPersistDbContext();
            weatherForecastModel = (from wfm in dbPersistDbContext.WeatherForecastMasterList.Where(wfm => wfm.Id == masterId)
                                   join wfd in dbPersistDbContext.WeatherForecastDetailList.ToList()
                                   on wfm.Id equals wfd.WeatherForecastMasterId
                                   select new WeatherForecastModel
                                   {
                                       WFMasterId = wfm.Id,
                                       WFDetailId = wfd.Id,
                                       LastUpdated= wfd.UpdatedDateTime,
                                       WeatherForecast=wfd.WeatherForecast,
                                   }).Distinct().ToList();
            return weatherForecastModel;
        }

    }
}