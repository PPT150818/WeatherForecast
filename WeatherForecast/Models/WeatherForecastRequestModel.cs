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

namespace WeatherForecast.Models
{
    public static class WeatherForecastRequestModel
    {
        public static string ApiUrl { get; set; } = GetBaseUrl();

        private static string GetBaseUrl()
        {
            return "https://localhost:44345/";
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
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _httpClient.Timeout = new TimeSpan(1, 0, 0);
                }
                return _httpClient;
            }
            set
            {
                _httpClient = value;
            }
        }

        public static WeatherForecastModel GetWeatherForcastLiveData(WeatherForecast.Response.InterfaceModels.WeatherForecastInput weatherForecastInput)
        {
            HttpResponseMessage response = null;
            WeatherForecastModel weatherForecastModel = new WeatherForecastModel();
            WeatherForecast.Response.InterfaceModels.WeatherForecastResponse weatherForecast = new Response.InterfaceModels.WeatherForecastResponse();


            try
            {
                LogManager.GetLogger(typeof(WeatherForecastRequestModel)).Info("Get ");
               


                #region Get the axys label mapping settings

                try
                {
                    // Create a web request for fetching the axys - Clientport field mappings from settings to bind dynamic values
                    HttpClient = null;
                    var content = new StringContent(JsonConvert.SerializeObject(weatherForecastInput), Encoding.UTF8, "application/json");
                    response = HttpClient.PostAsync(HttpClient.BaseAddress + "GetWeatherForcastDetails", content).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        weatherForecast = JsonConvert.DeserializeObject<WeatherForecast.Response.InterfaceModels.WeatherForecastResponse>(result);

                        //if (hpslist == null)
                        //{
                        //    ResultString = "Please check if Client Portal Server is running. Not getting response from web API services.";
                        //    return APIResult.NullResult;
                        //}
                        //if (hpslist != null && hpslist.Count == 1 && hpslist[0].PMSFieldName == null)
                        //{
                        //    ResultString = "Account import Failed, unable to get axys label mapping from Client Portal.";
                        //    return APIResult.NullResult;
                        //}
                    }
                    //else
                    //{
                    //    LogManager.GetLogger(typeof(AxysMasterData)).Info(response.StatusCode.ToString() + " : " + response.Content.ReadAsStringAsync().Result.GetTrimValue());
                    //    ResultString = response.StatusCode.ToString() + " :" + response.Content.ReadAsStringAsync().Result.GetTrimValue();

                    //    return APIResult.InnerCodeError;
                    //}
                }
                catch (Exception ex)
                {
                    //LogManager.GetLogger(typeof(AxysMasterData)).Error("Account import failed " + Environment.NewLine + ex);
                    //ResultString = "Account import failed . " + Environment.NewLine + ex.InnerException.Message;
                    //return APIResult.Failure;
                }

                #endregion Get the axys label mapping settings
            }
            catch (Exception ex)
            {
                //LogManager.GetLogger(typeof(AxysMasterData)).Error("UploadAccountData Error : " + ex.StackTrace);
                //APIResult result = AxysHelper.GetLoggerResult(ex, cnt, negativeCnt, totalCnt, "UploadAccountData");
                //return result;
            }
            finally
            {
                //childMultipleProgressBar.CloseProgress();
            }
            return weatherForecastModel;
        }
    }
}