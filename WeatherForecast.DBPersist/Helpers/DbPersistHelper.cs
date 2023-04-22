using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.DbPersist.Helpers
{
    public static class DbPersistHelper
    {
        // Following property is added only for Azure Functions
        public static string ConnectionString { get; set; }
        public static string GetConnectionString()
        {
            string connectionString = string.Empty;

            foreach (ConnectionStringSettings connString in ConfigurationManager.ConnectionStrings)
            {
                if (connString.Name == "weatherForcastDbConnection")
                {
                    connectionString = connString.ConnectionString;
                    break;
                }
            }

            // Following line is applicable only if passing string
            if (string.IsNullOrEmpty(connectionString))
                connectionString = ConnectionString;

            return connectionString;
        }
    }
}
