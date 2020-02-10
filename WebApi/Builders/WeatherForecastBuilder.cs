using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Library;
using static Library.Entities.WeatherForecast;
using BusinessWeatherForecast = Library.Entities.WeatherForecast;

namespace WebApi.Builders
{
    public class WeatherForecastBuilder
    {
        public WeatherForecast BuildDataWeatherForecast(BusinessWeatherForecast weatherForecast)
        {
            return new WeatherForecast
            {
                Date = weatherForecast.Date.ToString("dd/MM/yyyy"),
                Day = weatherForecast.Day,
                Prediction = weatherForecast.Prediction.GetEnumDescription()
            };
        }

        public BusinessWeatherForecast BuildBusinessWeatherForecast(WeatherForecast weatherForecast)
        {
            return new BusinessWeatherForecast
            {
                Date = Convert.ToDateTime(weatherForecast.Date),
                Day = weatherForecast.Day,
                Prediction = (Weather)Enum.Parse(typeof(Weather), weatherForecast.Prediction, true) 
            };
        }
    }
}
