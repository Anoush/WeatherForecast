
using MongoDB.Driver;

namespace Data.Models
{
    public class WeatherForecastDatabaseSettings :  IWeatherForecastDatabaseSettings
    {
        public string WeatherForecastCollectionName { get;  set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public SslSettings SslSettings { get; set; }
    }
}
