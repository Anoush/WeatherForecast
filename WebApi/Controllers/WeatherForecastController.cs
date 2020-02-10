using Data.Services;
using Library;
using Library.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Builders;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastService _weatherForecastService;
        private readonly WeatherForecastBuilder _weatherForecastBuilder;

        private WeatherForecastBuilder WeatherForecastBuilder { get => _weatherForecastBuilder; }

        public WeatherForecastController(WeatherForecastService weatherForecastService, WeatherForecastBuilder weatherForecastBuilder)
        {
            _weatherForecastService = weatherForecastService;
            _weatherForecastBuilder = weatherForecastBuilder;
        }

        //[HttpGet(Name = nameof(GetWeatherForecast))]
        //public ActionResult GetWeatherForecast(int? dia)
        //{
        //    Processor system = InitializeSystem();
        //    var predictions = new List<WeatherResult>();

        //    if (dia != null)
        //        predictions.Add(MapWeatherForecastObjectToWeatherResult(system.CalculateConditionsForADay((int)dia)));
        //    else
        //        system.GetAllWeatherForecast().ForEach(item => predictions.Add(MapWeatherForecastObjectToWeatherResult(item)));

        //    return Ok(predictions);
        //}


        [HttpGet]
        public ActionResult<List<WeatherForecast>> Get() {
        
            var weatherForecastList = _weatherForecastService.Get();
            var returnData = new List<WeatherForecast>();
            weatherForecastList.ForEach(item => returnData.Add(WeatherForecastBuilder.BuildBusinessWeatherForecast(item)));
            return returnData;
        }

        [HttpGet("{day:length(24)}", Name = "GetWeatherForecast")]
        public ActionResult<WeatherForecast> Get(string day)
        {
            var weatherforecast = _weatherForecastService.Get(day);

            if (weatherforecast == null)
            {
                return NotFound();
            }

            return WeatherForecastBuilder.BuildBusinessWeatherForecast(weatherforecast);
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Create(WeatherForecast weatherforecast)
        {
            var weatherforecastobj = WeatherForecastBuilder.BuildDataWeatherForecast(weatherforecast);
            _weatherForecastService.Create(weatherforecastobj);

            return CreatedAtRoute("GetWeatherForecast", new { id = weatherforecastobj.Id.ToString() }, weatherforecast);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, WeatherForecast weatherforecast)
        {
            var weatherforecastIn = WeatherForecastBuilder.BuildDataWeatherForecast(weatherforecast);
            
            if (_weatherForecastService.Get(id) == null)
            {
                return NotFound();
            }

            _weatherForecastService.Update(id, weatherforecastIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var weatherforecast = _weatherForecastService.Get(id);

            if (weatherforecast == null)
            {
                return NotFound();
            }

            _weatherForecastService.Remove(weatherforecast.Id);

            return NoContent();
        }

        private static Processor InitializeSystem()
        {
            var planet1 = new Planet(500, "Ferrengi", 1);
            var planet2 = new Planet(2000, "Betasoide", 3);
            var planet3 = new Planet(1000, "Vulcano", -5);

            var system = new Processor();
            system.Planets.AddRange(new[] { planet1, planet2, planet3 });

            return system;
        }

        private WeatherResult MapWeatherForecastObjectToWeatherResult(WeatherForecast weatherForecast)
        {
            return new WeatherResult
            {
                Day = weatherForecast.Day,
                Date = weatherForecast.Date.ToString("dd/MM/yyyy"),
                Weather = weatherForecast.Prediction.GetEnumDescription()
            };
        }
    }
}
