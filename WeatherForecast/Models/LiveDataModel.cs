using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WeatherForecast.Response.InterfaceModels;

namespace WeatherForecast.Models
{
    public class LiveDataModel
    {
        private WeatherForecastInput _weatherForecastInput;
        public WeatherForecastInput WeatherForecastInput
        {
            get
            {
                if (_weatherForecastInput == null)
                {
                    _weatherForecastInput = new WeatherForecastInput();
                    
                }
                return _weatherForecastInput;
            }
            set
            {
                _weatherForecastInput = value;
            }
        }

        private WeatherForecastModel _weatherForecastModel;
        public WeatherForecastModel WeatherForecastModel
        {
            get
            {
                if (_weatherForecastModel == null)
                {
                    _weatherForecastModel = new WeatherForecastModel();

                }
                return _weatherForecastModel;
            }
            set
            {
                _weatherForecastModel = value;
            }
        }
    }
}