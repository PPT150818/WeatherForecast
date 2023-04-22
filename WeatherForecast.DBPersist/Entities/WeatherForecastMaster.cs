using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.DbPersist.Entities
{
    [Table("WeatherForecastMaster")]
    public partial class WeatherForecastMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string City { get; set; }
        public DateTime UpdatedDateTime { get; set; } = DateTime.Now;
        public virtual ICollection<WeatherForecastDetails> WeatherForecastDetails { get; set; }
    }
}
