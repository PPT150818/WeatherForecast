using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Globalization;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.IO;
using System.Web;
using WeatherForecast.Response.OpenMeteo;

namespace WeatherForecast.API.Models
{
    /// <summary>
    /// Handles GET Requests and performs API Calls.
    /// </summary>
    public class OpenMeteoClient
    {
        private readonly string _weatherApiUrl = "https://api.open-meteo.com/v1/forecast";
        private readonly HttpClientHelper httpController;

        /// <summary>
        /// Creates a new <seealso cref="OpenMeteoClient"/> object and sets the neccessary variables (httpController, CultureInfo)
        /// </summary>
        public OpenMeteoClient()
        {
            httpController = new HttpClientHelper();
        }

      
        /// <summary>
        /// Performs one GET-Request
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Awaitable Task containing WeatherForecast or NULL</returns>
        public async Task<WeatherForecast.Response.OpenMeteo.WeatherForecast> QueryAsync(WeatherForecastOptions options)
        {
            try
            {
                return await GetWeatherForecastAsync(options);
            }
            catch (Exception)
            {
                return null;
            }
        }




        public WeatherForecast.Response.OpenMeteo.WeatherForecast QueryWeatherForecast(float latitude, float longitude)
        {
            return QueryAsync(latitude, longitude).GetAwaiter().GetResult();
        }

       


        /// <summary>
        /// Performs one GET-Request to get weather information
        /// </summary>
        /// <param name="latitude">City latitude</param>
        /// <param name="longitude">City longitude</param>
        /// <returns>Awaitable Task containing WeatherForecast or NULL</returns>
        public async Task<WeatherForecast.Response.OpenMeteo.WeatherForecast> QueryAsync(float latitude, float longitude)
        {
            WeatherForecastOptions options = new WeatherForecastOptions
            {
                Latitude = latitude,
                Longitude = longitude,
                Current_Weather = true
            };
            return await QueryAsync(options);
        }

        public WeatherForecast.Response.OpenMeteo.WeatherForecast Query(WeatherForecastOptions options)
        {
            return QueryAsync(options).GetAwaiter().GetResult();
        }        

        private async Task<WeatherForecast.Response.OpenMeteo.WeatherForecast> GetWeatherForecastAsync(WeatherForecastOptions options)
        {
            try
            {
                var response = httpController.Client.GetAsync(MergeUrlWithOptions(_weatherApiUrl, options));
                var contentStream = await response.Result.Content.ReadAsStreamAsync();
                //response.EnsureSuccessStatusCode();
                //string responseString = response.Result.Content.ReadAsStringAsync().Result;
                //WeatherForecast.API.OpenMeteo.WeatherForecast weatherForecast =  JsonConvert.DeserializeObject<WeatherForecast.API.OpenMeteo.WeatherForecast>(responseString);


                //var streamReader = new StreamReader(contentStream);
                //var jsonReader = new JsonTextReader(streamReader);

                return await JsonSerializer.DeserializeAsync<WeatherForecast.Response.OpenMeteo.WeatherForecast>(contentStream, new System.Text.Json.JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });
                
                //WeatherForecast.API.OpenMeteo.WeatherForecast weatherForecast1 = await JsonSerializer.DeserializeAsync<WeatherForecast.API.OpenMeteo.WeatherForecast>(streamReader., new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                //WeatherForecast.API.OpenMeteo.WeatherForecast weatherForecast1 = JsonSerializer.Deserialize<WeatherForecast.API.OpenMeteo.WeatherForecast>(utf8Json:streamReader);
                //return Task.FromResult(weatherForecast);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        //private async Task<GeocodingApiResponse?> GetGeocodingDataAsync(GeocodingOptions options)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await httpController.Client.GetAsync(MergeUrlWithOptions(_geocodeApiUrl, options));
        //        response.EnsureSuccessStatusCode();

        //        GeocodingApiResponse? geocodingData = await JsonSerializer.DeserializeAsync<GeocodingApiResponse>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        //        return geocodingData;
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        Console.WriteLine("Can't find " + options.Name + ". Please make sure that the name is valid.");
        //        Console.WriteLine(e.Message);
        //        return null;
        //    }
        //}

        private string MergeUrlWithOptions(string url, WeatherForecastOptions options)
        {
            if (options == null) return url;

            string optionQuery=string.Empty;
           
            bool isFirstParam = false;

            // If no query given, add '?' to start the query string
            if (string.IsNullOrEmpty(optionQuery))
            {
                optionQuery = "?";

                // isFirstParam becomes true because the query string is new
                isFirstParam = true;
            }

            // Add the properties
            
            // Begin with Latitude and Longitude since they're required
            if (isFirstParam)
                optionQuery += "latitude=" +  options.Latitude.ToString(CultureInfo.InvariantCulture);
            else
                optionQuery += "&latitude=" + options.Latitude.ToString(CultureInfo.InvariantCulture);

            optionQuery += "&longitude=" + options.Longitude.ToString(CultureInfo.InvariantCulture);

            optionQuery += "&temperature_unit=" + options.Temperature_Unit.ToString();
            optionQuery += "&windspeed_unit=" + options.Windspeed_Unit.ToString();
            optionQuery += "&precipitation_unit=" + options.Precipitation_Unit.ToString();
            if (options.Timezone != string.Empty)
                optionQuery += "&timezone=" + options.Timezone;

            optionQuery += "&current_weather=" + options.Current_Weather;

            optionQuery += "&timeformat=" + options.Timeformat.ToString();

            optionQuery += "&past_days=" + options.Past_Days;

            if (options.Start_date != string.Empty)
                optionQuery += "&start_date=" + options.Start_date;
            if (options.End_date != string.Empty)
                optionQuery += "&end_date=" + options.End_date;

            // Now we iterate through hourly and daily

            // Hourly
            if (options.Hourly.Count > 0)
            {
                bool firstHourlyElement = true;
                optionQuery += "&hourly=";

                foreach (var option in options.Hourly)
                {
                    if (firstHourlyElement)
                    {
                        optionQuery += option.ToString();
                        firstHourlyElement = false;
                    }
                    else
                    {
                        optionQuery += "," + option.ToString();
                    }
                }
            }

            // Daily
            if (options.Daily.Count > 0)
            {
                bool firstDailyElement = true;
                optionQuery += "&daily=";
                foreach (var option in options.Daily)
                {
                    if (firstDailyElement)
                    {
                        optionQuery += option.ToString();
                        firstDailyElement = false;
                    }
                    else
                    {
                        optionQuery += "," + option.ToString();
                    }
                }
            }

            // 0.2.0 Weather models
            // cell_selection
            optionQuery += "&cell_selection=" + options.Cell_Selection;

            // Models
            if (options.Models.Count > 0)
            {
                bool firstModelsElement = true;
                optionQuery += "&models=";
                foreach (var option in options.Models)
                {
                    if (firstModelsElement)
                    {
                        optionQuery += option.ToString();
                        firstModelsElement = false;
                    }
                    else
                    {
                        optionQuery += "," + option.ToString();
                    }
                }
            }
            UriBuilder uri = new UriBuilder(url+optionQuery);
            return uri.ToString();
        }

        /// <summary>
        /// Combines a given url with an options object to create a url for GET requests
        /// </summary>
        /// <returns>url+queryString</returns>
        //private string MergeUrlWithOptions(string url, GeocodingOptions options)
        //{
        //    if (options == null) return url;

        //    UriBuilder uri = new UriBuilder(url);
        //    bool isFirstParam = false;

        //    // If no query given, add '?' to start the query string
        //    if (uri.Query == string.Empty)
        //    {
        //        uri.Query = "?";

        //        // isFirstParam becomes true because the query string is new
        //        isFirstParam = true;
        //    }

        //    // Now we check every property and set the value, if neccessary
        //    if (isFirstParam)
        //        uri.Query += "name=" + options.Name;
        //    else
        //        uri.Query += "&name=" + options.Name;

        //    if(options.Count >0)
        //        uri.Query += "&count=" + options.Count;
            
        //    if (options.Format != string.Empty)
        //        uri.Query += "&format=" + options.Format;

        //    if (options.Language != string.Empty)
        //        uri.Query += "&language=" + options.Language;

        //    return uri.ToString();
        //}

        ///// <summary>
        ///// Combines a given url with an options object to create a url for GET requests
        ///// </summary>
        ///// <returns>url+queryString</returns>
        //private string MergeUrlWithOptions(string url, AirQualityOptions options)
        //{
        //    if (options == null) return url;

        //    UriBuilder uri = new UriBuilder(url);
        //    bool isFirstParam = false;

        //    // If no query given, add '?' to start the query string
        //    if (uri.Query == string.Empty)
        //    {
        //        uri.Query = "?";

        //        // isFirstParam becomes true because the query string is new
        //        isFirstParam = true;
        //    }

        //    // Now we check every property and set the value, if neccessary
        //    if (isFirstParam)
        //        uri.Query += "latitude=" + options.Latitude.ToString(CultureInfo.InvariantCulture);
        //    else
        //        uri.Query += "&latitude=" + options.Latitude.ToString(CultureInfo.InvariantCulture);

        //    uri.Query += "&longitude=" + options.Longitude.ToString(CultureInfo.InvariantCulture);

        //    if (options.Domains != string.Empty)
        //        uri.Query += "&domains=" + options.Domains;

        //    if (options.Timeformat != string.Empty)
        //        uri.Query += "&timeformat=" + options.Timeformat;

        //    if (options.Timezone != string.Empty)
        //        uri.Query += "&timezone=" + options.Timezone;

        //    // Finally add hourly array
        //    if (options.Hourly.Count >= 0)
        //    {
        //        bool firstHourlyElement = true;
        //        uri.Query += "&hourly=";

        //        foreach (var option in options.Hourly)
        //        {
        //            if (firstHourlyElement)
        //            {
        //                uri.Query += option.ToString();
        //                firstHourlyElement = false;
        //            }
        //            else
        //            {
        //                uri.Query += "," + option.ToString();
        //            }
        //        }
        //    }

        //    return uri.ToString();
        //}
    }
}

