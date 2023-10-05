using Newtonsoft.Json;
using System.Threading.Tasks;
using WeatherWebapi.Data;
using WeatherWebapi.Models.Response;
using WeatherWebapi.Services.HttpClientWrapper;

namespace WeatherWebapi.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientWrapper httpClient;

        public WeatherService(IHttpClientWrapper httpClient)
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
            if (weatherResponse == null)
            {
                return null;
            }

            WeatherData weatherData = new WeatherData(weatherResponse);

            return weatherData;
        }
    }
}
