using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DemoCoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // Wrtie Put
        [HttpPut] // => 當有使用到 Swagger UI 的時候，要加上去。 或是你的 Action 名稱不是 Http 動詞的時候，也要加上去
        public void Put()
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
        }
        // Wrtie Delete

        // Wrtie Patch
    }
}
