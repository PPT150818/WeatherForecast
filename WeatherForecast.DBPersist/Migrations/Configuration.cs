using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.DbPersist.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DbPersistDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WeatherForecast.DbPersist.DbPersistDbContext";
            CommandTimeout = 300;
        }

        protected override void Seed(DbPersistDbContext context)
        {
            context.SaveChanges();
        }

        private static List<string> Load(string name)
        {
            var assembaly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembaly.GetManifestResourceStream(name))
            using (StreamReader reader = new StreamReader(stream))
            {

                string results = reader.ReadToEnd();
                return GetStoredProcedure(results);
            }
        }

        private static List<string> GetStoredProcedure(string result)
        {

            List<string> str = new List<string>(
            result.Split(new string[] { "go" }, StringSplitOptions.None));
            return str;
        }

        private static void ExecuteStoredProcedure(List<string> procedures, DbPersistDbContext context)
        {
            foreach (string str in procedures)
            {
                try
                {
                    context.Database.ExecuteSqlCommand(str);
                }
                catch (Exception ex)
                {
                    //LogManager.GetLogger(typeof(Configuration)).Error(ex.StackTrace);
                }
            }

        }
    }
}
