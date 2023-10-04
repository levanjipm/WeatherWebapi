using System.Threading.Tasks;
using WeatherWebapi.Data;

namespace WeatherWebapi.Services.Weather
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(string city);
    }
}
