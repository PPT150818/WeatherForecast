using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Web.UI.WebControls;
using log4net;
using System.Text;
using WeatherForecast.Response.InterfaceModels;
using WeatherForecast.DbPersist.Entities;
using WeatherForecast.Controllers;
using System.Configuration;

namespace WeatherForecast.Models
{
    public static class WeatherForecastRequestModel
    {
        public static string ApiUrl { get; set; } = GetBaseUrl();

        private static string GetBaseUrl()
        {
            return WeatherForcastAPIURL();
        }

        private static HttpClient _httpClient;
        public static HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    _httpClient = new HttpClient();
                    _httpClient.BaseAddress = new Uri(ApiUrl, UriKind.RelativeOrAbsolute);
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _httpClient.Timeout = new TimeSpan(1, 0, 0);
                }
                return _httpClient;
            }
            set
            {
                _httpClient = value;
            }
        }
        public static string WeatherForcastAPIURL()
        {
            string url = ConfigurationManager.AppSettings.AllKeys.Contains("WeatherForcastApiUrl") ? ConfigurationManager.AppSettings["WeatherForcastApiUrl"] : string.Empty;
            if (!string.IsNullOrWhiteSpace(url) && !url.EndsWith("/"))
            {
                LogManager.GetLogger(typeof(ForecastWeatherController)).Info("WeatherForcastApiUrl : "+ url);
                url = url + "/";
            }
            else
                LogManager.GetLogger(typeof(ForecastWeatherController)).Error("no url configured in web.config for API");

            return url;
        }
        public static WeatherForecastModel GetWeatherForcastLiveData(WeatherForecast.Response.InterfaceModels.WeatherForecastInput weatherForecastInput)
        {
            WeatherForecastModel weatherForecastModel = null;

            LogManager.GetLogger(typeof(WeatherForecastRequestModel)).Info("Get Weather Forcast Live Data ");

            try
            {
                // Create a web request for fetching 
                HttpClient = null;
                var content = new StringContent(JsonConvert.SerializeObject(weatherForecastInput), Encoding.UTF8, "application/json");
                HttpResponseMessage response = HttpClient.PostAsync(HttpClient.BaseAddress + "api/WeatherForcast/GetWeatherForcastDetails", content).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    weatherForecastModel = new WeatherForecastModel();
                    string result = response.Content.ReadAsStringAsync().Result;
                    WeatherForecastResponse weatherForecast = JsonConvert.DeserializeObject<WeatherForecastResponse>(result);
                    weatherForecastModel.WeatherForecast = weatherForecast.weatherForecast;
                    if (weatherForecast.weatherForecast==null || !SaveAPIDetailResponse(weatherForecastInput, weatherForecastModel))
                    {
                        LogManager.GetLogger(typeof(ForecastWeatherController)).Error("Failed to get response and save enties in db");
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return weatherForecastModel;
        }

        internal static bool SaveAPIDetailResponse(WeatherForecastInput weatherForecastInput, WeatherForecastModel weatherForecastResponse)
        {
            try
            {
                if (weatherForecastResponse.WeatherForecast != null)
                {
                    DbPersist.DbPersistDbContext dbPersistDbContext = new DbPersist.DbPersistDbContext();
                    List<WeatherForecastMaster> weatherForecastMasters = dbPersistDbContext.WeatherForecastMasterList.ToList();

                    WeatherForecastMaster weatherForecastMaster = weatherForecastMasters.FirstOrDefault(wfm => wfm.Id == weatherForecastInput.WFMasterId);
                    if (weatherForecastMaster == null)
                    {
                        weatherForecastMaster = weatherForecastMasters.FirstOrDefault(wfm => wfm.Latitude == weatherForecastInput.Latitude && wfm.Longitude == weatherForecastInput.Longitude);
                        if (weatherForecastMaster == null)
                        {
                            weatherForecastMaster = new WeatherForecastMaster();
                            weatherForecastMaster.Latitude = weatherForecastInput.Latitude.HasValue ? weatherForecastInput.Latitude.Value : 0;
                            weatherForecastMaster.Longitude = weatherForecastInput.Longitude.HasValue ? weatherForecastInput.Longitude.Value : 0;
                            weatherForecastMaster.UpdatedDateTime = DateTime.Now;
                        }

                    }
                    WeatherForecastDetails weatherForecastDetails = new WeatherForecastDetails();
                    weatherForecastDetails.WeatherForecast = JsonConvert.SerializeObject(weatherForecastResponse.WeatherForecast);
                    weatherForecastDetails.UpdatedDateTime= DateTime.Now;
                    weatherForecastDetails.WeatherForecastMaster = weatherForecastMaster;
                    dbPersistDbContext.WeatherForecastDetailList.Add(weatherForecastDetails);
                    dbPersistDbContext.SaveChanges();
                    weatherForecastResponse.WFMasterId = weatherForecastMaster.Id;
                    weatherForecastResponse.WFDetailId = weatherForecastDetails.Id;
                    return true;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
    }
}