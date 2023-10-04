using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherWebapi.Data;
using WeatherWebapi.Models.Response;

namespace WeatherWebapi.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient httpClient;

        public WeatherService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            string apiUrl = string.Format(Utility.Constants.WEATHER_API_URL, city, Utility.Constants.API_KEY);
            var response = await httpClient.GetStringAsync(apiUrl);
            if (response == null)
            {
                return null;
            }

            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
            WeatherData weatherData = new WeatherData(weatherResponse);

            return weatherData;
        }
    }
}
