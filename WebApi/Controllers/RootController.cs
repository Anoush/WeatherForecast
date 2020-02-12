using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        public ActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                wheatherForecast = new
                {
                   href = Url.Link(nameof(WeatherForecastController.GetWeatherForecast), null)
                },
                generateWeatherForecast = new
                {
                    href = Url.Link(nameof(WeatherForecastController.GenerateWeatherForecast), null)
                },
                clearWeatherForecast = new
                {
                    href = Url.Link(nameof(WeatherForecastController.ClearWeatherForecast), null)
                }
            };
            return Ok(response);
        }
    }
}
