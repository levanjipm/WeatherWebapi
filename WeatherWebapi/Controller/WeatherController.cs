using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherWebapi.Data;
using WeatherWebapi.Models;
using WeatherWebapi.Services.Weather;

namespace WeatherWebapi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("city/{selectedCity}")]
        public async Task<ActionResult<WeatherData>> GetWeather(string selectedCity)
        {
            var weatherData = await _weatherService.GetWeatherAsync(selectedCity);
            return Ok(weatherData);
        }
    }
}
