using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherForecast.Response.InterfaceModels;
using WeatherForecast.DbPersist;
using Newtonsoft.Json;
using System.Collections;
using WeatherForecast.Helpers;
using WeatherForecast.DbPersist.Entities;

namespace WeatherForecast.Models
{

    public class WeatherForecastModel
    {
        public int WFMasterId { get; set; }
        public int WFDetailId { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Showbutton { get; set; } = true;
        public WeatherForecast.Response.OpenMeteo.WeatherForecast WeatherForecast { get; set; }

        internal CurrentDetails ToCurrentForecast()
        {
            CurrentDetails currentModel = new CurrentDetails();
            if (this.WeatherForecast.CurrentWeather == null)
            {
                this.WeatherForecast.CurrentWeather = new Response.OpenMeteo.CurrentWeather();

            }
            currentModel.WFMasterId = this.WFMasterId;
            currentModel.WFDetailId = this.WFDetailId;
            currentModel.LastUpdated = this.LastUpdated;
            currentModel.WeatherForecast = this.WeatherForecast;
            currentModel.Weathercode = this.WeatherForecast.CurrentWeather.Weathercode.WeathercodeToString();
            currentModel.WindDirection = this.WeatherForecast.CurrentWeather.WindDirection;
            currentModel.Temperature = this.WeatherForecast.CurrentWeather.Temperature;
            currentModel.Windspeed = this.WeatherForecast.CurrentWeather.Windspeed;
            currentModel.Timezone = this.WeatherForecast.Timezone;
            currentModel.GenerationTime = this.WeatherForecast.GenerationTime;
            currentModel.Time = this.WeatherForecast.CurrentWeather.Time;

            return currentModel;
        }
    }

    public class CurrentDetails : WeatherForecastModel
    {

        public string Time { get; set; }
        public float Temperature { get; set; }
        public string Weathercode { get; set; }
        public float Windspeed { get; set; }
        public float WindDirection { get; set; }
        public string Timezone { get; set; }

        public float GenerationTime { get; set; }

    }
    public class HistoricalDataModel
    {
        public ArrayList weatherForecastDetailModel { get; set; }
        public ArrayList weatherForecastMasterModel { get; set; }
        public WeatherForecastModel weatherForecastLiveDataModel { get; set; }

        public int MasterKey { get; set; }
        public int DetailKey { get; set; }
        public ArrayList GetWeatherForecastMasterModel()
        {
            ArrayList weatherForecastModel = new ArrayList();
            DbPersistDbContext dbPersistDbContext = new DbPersistDbContext();
            weatherForecastModel = dbPersistDbContext.WeatherForecastMasterList.Select(wfm => new WeatherForecastModel { WFMasterId = wfm.Id, Latitude = wfm.Latitude, Longitude = wfm.Longitude, LastUpdated = wfm.UpdatedDateTime }).OrderByDescending(s => s.WFMasterId).Distinct().ToList().ToArrayList();
            return weatherForecastModel == null ? new ArrayList() : weatherForecastModel;
        }

        public ArrayList GetWeatherForecastDetailModel(int masterId)
        {
            ArrayList weatherForecastModel = new ArrayList();

            DbPersistDbContext dbPersistDbContext = new DbPersistDbContext();
            var weatherForecasts = dbPersistDbContext.WeatherForecastDetailList.ToList().Where(wd => wd.WeatherForecastMasterId == masterId).ToList();

            foreach (var item in weatherForecasts.OrderByDescending(s => s.Id))
            {
                CurrentDetails weatherForecastModel1 = new CurrentDetails();

                weatherForecastModel1.WFMasterId = item.WeatherForecastMasterId;
                weatherForecastModel1.WFDetailId = item.Id;
                weatherForecastModel1.LastUpdated = item.UpdatedDateTime;
                weatherForecastModel1.WeatherForecast = JsonConvert.DeserializeObject<WeatherForecast.Response.OpenMeteo.WeatherForecast>(item.WeatherForecast);
                if (weatherForecastModel1.WeatherForecast.CurrentWeather == null)
                {
                    weatherForecastModel1.WeatherForecast.CurrentWeather = new Response.OpenMeteo.CurrentWeather();
                }
                weatherForecastModel1.Weathercode = weatherForecastModel1.WeatherForecast.CurrentWeather.Weathercode.WeathercodeToString();
                weatherForecastModel1.WindDirection = weatherForecastModel1.WeatherForecast.CurrentWeather.WindDirection;
                weatherForecastModel1.Temperature = weatherForecastModel1.WeatherForecast.CurrentWeather.Temperature;
                weatherForecastModel1.Windspeed = weatherForecastModel1.WeatherForecast.CurrentWeather.Windspeed;
                weatherForecastModel1.Timezone = weatherForecastModel1.WeatherForecast.Timezone;
                weatherForecastModel1.GenerationTime = weatherForecastModel1.WeatherForecast.GenerationTime;
                weatherForecastModel1.Time = weatherForecastModel1.WeatherForecast.CurrentWeather.Time;
                weatherForecastModel.Add(weatherForecastModel1);
            }
            return weatherForecastModel;
        }

        public HistoricalDataModel GetHistoricalDetails()
        {
            weatherForecastMasterModel = GetWeatherForecastMasterModel();
            WeatherForecastModel weatherForecastModel = null;
            if (weatherForecastMasterModel != null )
            {
                weatherForecastModel = weatherForecastMasterModel.ToList<WeatherForecastModel>().OrderByDescending(s => s.LastUpdated).FirstOrDefault();
                int masterId = weatherForecastModel != null ? weatherForecastModel.WFMasterId : 0;
                weatherForecastDetailModel = GetWeatherForecastDetailModel(masterId);

                this.weatherForecastMasterModel = weatherForecastMasterModel.ToList<WeatherForecastModel>().OrderByDescending(s => s.WFMasterId).ToList().ToArrayList();
                this.weatherForecastDetailModel = weatherForecastDetailModel.ToList<WeatherForecastModel>().OrderByDescending(s => s.WFDetailId).ToList().ToArrayList();
                this.weatherForecastLiveDataModel = weatherForecastDetailModel.ToList<WeatherForecastModel>().OrderByDescending(s => s.WFDetailId).ToList().FirstOrDefault();
                this.weatherForecastLiveDataModel = weatherForecastLiveDataModel == null ? new WeatherForecastModel() : weatherForecastLiveDataModel;
                this.MasterKey = weatherForecastLiveDataModel != null ? weatherForecastLiveDataModel.WFMasterId : 0;
                this.DetailKey = weatherForecastLiveDataModel != null ? weatherForecastLiveDataModel.WFDetailId : 0;
            }
            else
            {
                weatherForecastDetailModel = new ArrayList();
            }

            

            return this;
        }

        internal HistoricalDataModel GetHistoricalDetailsData(WeatherForecastInput weatherForecastInput, bool getlivedata=true)
        {
            WeatherForecastModel weatherForecastModel = null;
            //weatherForecastModel = weatherForecastMasterModel.ToList<WeatherForecastModel>().OrderByDescending(s => s.LastUpdated).FirstOrDefault();
            weatherForecastDetailModel = GetWeatherForecastDetailModel(weatherForecastInput.WFMasterId);
            if (getlivedata)
            {
                weatherForecastModel = WeatherForecastRequestModel.GetWeatherForcastLiveData(new WeatherForecastInput { Latitude = weatherForecastInput.Latitude, Longitude = weatherForecastInput.Longitude, WFMasterId = weatherForecastInput.WFMasterId });
                weatherForecastModel=weatherForecastModel.ToCurrentForecast();
                this.weatherForecastDetailModel.Add(weatherForecastModel);
            }
            else
                weatherForecastModel = weatherForecastDetailModel.ToList<WeatherForecastModel>().FirstOrDefault();
            this.weatherForecastDetailModel = weatherForecastDetailModel.ToList<WeatherForecastModel>().OrderByDescending(s => s.WFDetailId).ToList().ToArrayList();
            this.weatherForecastLiveDataModel = weatherForecastModel == null ? new WeatherForecastModel() : weatherForecastModel;
            this.MasterKey = weatherForecastLiveDataModel.WFMasterId;
            this.DetailKey = weatherForecastLiveDataModel.WFDetailId;

            return this;
        }

        internal bool DeleteMasterDetails(int masterId)
        {
            DbPersistDbContext dbPersistDbContext = new DbPersistDbContext();
            var weatherForecasts = dbPersistDbContext.WeatherForecastMasterList.FirstOrDefault(wd => wd.Id == masterId);
            if (weatherForecasts != null)
            {
                dbPersistDbContext.WeatherForecastMasterList.Remove(weatherForecasts);
                dbPersistDbContext.SaveChanges();
            }
            return true;
        }

        internal object GetDetailsGridData(WeatherForecastInput weatherForecastInput)
        {
            throw new NotImplementedException();
        }
    }
}