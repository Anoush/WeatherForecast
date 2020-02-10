using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public interface IWeatherForecastDatabaseSettings
    {
        string WeatherForecastCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
