using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WeatherForecast.API.Models
{
    /// <summary>
    /// Initialised httpclient and required header parameters
    /// </summary>
    public class HttpClientHelper
    {
        public HttpClient Client { get { return _httpClient; } }
        private readonly HttpClient _httpClient;

        public HttpClientHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("om-dotnet");

        }
    }
}