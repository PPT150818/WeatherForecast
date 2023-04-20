using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.DbPersist.Entities
{
    [Table("CurrentWeather")]
    public partial class CurrentWeather
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Temperature { get; set; }
        public string Weathercode { get; set; }
        public string Windspeed { get; set; }
        public string WindDirection { get; set; }
        public virtual WeatherForecastDetails ForecastDetails { get; set; }
    }
}
