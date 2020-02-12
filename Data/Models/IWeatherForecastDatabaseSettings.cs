
using MongoDB.Driver;

namespace Data.Models
{
    public interface IWeatherForecastDatabaseSettings
    {
        string WeatherForecastCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        SslSettings SslSettings { get; set; }
    }
}
