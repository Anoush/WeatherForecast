using Data.Services;
using Library;
using Library.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using System;
using static WebApi.Models.ApiResultMessages;

namespace WebApi.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastController(WeatherForecastService weatherForecastService) 
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet(Name = nameof(GetWeatherForecast))]
        public ActionResult GetWeatherForecast(string day)
        {
            try
            {
                var weatherForecastList = new List<WeatherResult>();
                if (day != null)
                {
                    var weatherForecast = GetByDay(day.ToString());
                    if (weatherForecast != null) weatherForecastList.Add(GetByDay(day.ToString()));
                }
                else weatherForecastList = GetAll();

                if (weatherForecastList.Count == 0)
                    return Ok(new ApiResultMessages { Status = Statuses.Success.GetEnumDescription(), Message = Messages.NoDataFound.GetEnumDescription() });

                return Ok(weatherForecastList);
            }
            catch { return Ok(new ApiResultMessages { Status = Statuses.Success.GetEnumDescription(), Message = Messages.ServiceError.GetEnumDescription() }); }
            
        }

        [HttpGet(Name = nameof(GenerateWeatherForecast))]
        public ActionResult GenerateWeatherForecast()
        {
            var weatherForecastList = _weatherForecastService.Get();
            Processor system = new Processor();

            if (weatherForecastList.Count == 0)
                weatherForecastList = PopulateData(system);

            var conditionResults = system.CalculateConditionsForAPeriod(weatherForecastList).ToList();
            var resultData = new List<ReportModel>();
            conditionResults.ForEach(item => resultData.Add(Mapping.MapResultToReport(item)));
            return Ok(resultData);
        }

        [HttpGet(Name = nameof(ClearWeatherForecast))]
        public ActionResult ClearWeatherForecast()
        {
            try
            {
                CleanData();
                return Ok(new ApiResultMessages { Status = Statuses.Success.GetEnumDescription(), Message = Messages.CleanUpSuccess.GetEnumDescription() });
            }
            catch 
            { 
                return Ok(new ApiResultMessages { Status = Statuses.Error.GetEnumDescription(), Message = Messages.CleanUpError.GetEnumDescription() }); 
            }
            
        }

        private List<WeatherForecast> PopulateData(Processor system)
        {
            var weatherForecastList = system.GetAllWeatherForecast();
            weatherForecastList.ForEach(item => _weatherForecastService.Create(item));
            return weatherForecastList;
        }

        private List<WeatherForecast> CleanData()
        {
            var weatherForecastList = _weatherForecastService.Get();
            if (weatherForecastList.Count > 0)
                weatherForecastList.ForEach(item => _weatherForecastService.Remove(item));
            return weatherForecastList;
        }

        private List<WeatherResult> GetAll()        
        {
            var weatherForecastList = _weatherForecastService.Get();

            var returnData = new List<WeatherResult>();
            weatherForecastList.ForEach(item => returnData.Add(Mapping.MapObjectToModel(item)));
            return returnData;
        }

        private WeatherResult GetByDay(string day)
        {
            var weatherForecast = _weatherForecastService.Get(day);
            return weatherForecast != null ? Mapping.MapObjectToModel(weatherForecast) : null;
        }
    }
}
