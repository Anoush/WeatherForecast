using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library;

namespace WebApi.Models
{
    public static class Mapping
    {
        public static WeatherResult MapObjectToModel(WeatherForecast entity)
        {
            return new WeatherResult
            {
                Date = entity.Date.ToString("dd/MM/yyyy"),
                Day = entity.Day,
                Weather = entity.Prediction.GetEnumDescription()
            };
        }

        public static ReportModel MapResultToReport(KeyValuePair<string, string> result)
        {
            return new ReportModel
            {
                Weather = result.Key,
                Period = result.Value
            };
        }
    }
}
