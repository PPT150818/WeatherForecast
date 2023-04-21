using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.DbPersist.Entities;
using  WeatherForecast.DbPersist.Helpers;

namespace WeatherForecast.DbPersist
{
    public class DbPersistDbContext : DbContext
    {
        public DbPersistDbContext()
            : base(DbPersistHelper.GetConnectionString())
        {
        }
        public DbPersistDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.Initialize(false);
            //    BillPortDbContextFactory.ConnString = nameOrConnectionString;
            this.Database.CommandTimeout = 3600; // 1 hour for summary calculations
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
       
        public DbSet<WeatherForecastMaster> WeatherForecastMasterList  { get; set; }
        public DbSet<WeatherForecastDetails> WeatherForecastDetailList { get; set; }
        public DbSet<CurrentWeather> CurrentWeatherList { get; set; }
    }
}
