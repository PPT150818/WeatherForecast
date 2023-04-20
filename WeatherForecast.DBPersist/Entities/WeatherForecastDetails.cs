using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.DbPersist.Entities
{
    [Table("WeatherForecastDetails")]
    public partial class WeatherForecastDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Elevation { get; set; }
        public float GenerationTime { get; set; }
        public float UtcOffset { get; set; }
        public string Timezone { get; set; }
        public string TimezoneAbbreviation { get; set; }
        public string Hourly_units { get; set; }
        public string Hourly { get; set; }
        public string Daily_units { get; set; }
        public string Daily { get; set; }

        public virtual ICollection<CurrentWeather> CurrentWeathers
        {
            get; set;
        }

    }
}
