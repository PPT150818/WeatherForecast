
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;
using WeatherForecast.API.Models;

namespace WeatherForecast.Tests
{
    [TestClass()]
    public class LivedataTests
    {

        [TestMethod]
        public Task Latitude_Longitude_Test()
        {
            OpenMeteoClient client = new OpenMeteoClient();

            Response.OpenMeteo.WeatherForecast weatherData =  client.QueryWeatherForecast(1.125f, 2.25f);

            Assert.IsNotNull(weatherData);
            Assert.IsNotNull(weatherData.Longitude);
            Assert.IsNotNull(weatherData.Latitude);

            Assert.AreEqual(1.125f, weatherData.Latitude);
            Assert.AreEqual(2.25f, weatherData.Longitude);
            return Task.CompletedTask;
        }
    }
}