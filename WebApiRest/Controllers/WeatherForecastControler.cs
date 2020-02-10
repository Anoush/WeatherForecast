using Library;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRest.Controllers
{
    
    [Route("/")]
    [ApiController]
    public class WeatherForecastControler : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(int dia)
        {
            Processor system = InitializeSystem();
            var prediction = system.CalculateConditionsForADay(dia);
            var weatherForecast = new WeatherForecast
            {
                Dia = dia,
                Clima = prediction.GetEnumDescription()
            };
            return Ok(weatherForecast);
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
    }
}
