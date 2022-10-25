using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly CustomHttpClient _customHttpClient;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(CustomHttpClient customHttpClient, ILogger<WeatherForecastController> logger)
        {
            _customHttpClient = customHttpClient;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> Get()
        {
            var result = await _customHttpClient.GetAsync();
            return result;
        }
    }
}