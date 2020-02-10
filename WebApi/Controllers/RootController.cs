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
                   // href = Url.Link(nameof(WeatherForecastController.GetWeatherForecast),null)
                    href = Url.Link(nameof(WeatherForecastController.Get),null)
                }
            };
            return Ok(response);
        }
    }
}
