using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using WeatherWebapi.Services.Weather;
using Newtonsoft.Json;
using WeatherWebapi.Data;
using WeatherWebapi.Models.Response;
using System.Collections.Generic;
using System.Globalization;
using WeatherWebapi.Services.HttpClientWrapper;

namespace WeatherWebapi.Tests
{
    [TestClass]
    public class WeatherServiceTests
    {
        [TestMethod]
        public async Task GetWeatherAsync_ShouldReturnWeatherData()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            // Arrange
            var city = "London";
            WeatherResponse weatherResponse = _GenerateWeatherResponse();
            var expectedWeatherData = new WeatherData(weatherResponse);

            var mockHttpClient = new Mock<IHttpClientWrapper>();
            mockHttpClient.Setup(client => client.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(JsonConvert.SerializeObject(weatherResponse));

            var weatherService = new WeatherService(mockHttpClient.Object);

            // Act
            var result = await weatherService.GetWeatherAsync(city);

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(result.Location, expectedWeatherData.Location);
            Assert.AreEqual(result.TemperatureC, expectedWeatherData.TemperatureC);
            Assert.AreEqual(result.TemperatureF, expectedWeatherData.TemperatureF);
            Assert.AreEqual(result.Wind, expectedWeatherData.Wind);
            Assert.AreEqual(result.Pressure, expectedWeatherData.Pressure);
            Assert.AreEqual(result.SkyCondition, expectedWeatherData.SkyCondition);
            Assert.AreEqual(result.RelativeHumidity, expectedWeatherData.RelativeHumidity);
            Assert.AreEqual(result.Visibility, expectedWeatherData.Visibility);
        }

        [TestMethod]
        public async Task GetWeatherAsync_ShouldHandleNullResponse()
        {
            // Arrange
            var city = "NonExistentCity";

            var mockHttpClient = new Mock<IHttpClientWrapper>();
            mockHttpClient.Setup(client => client.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync((string)null); // Simulate a null response

            var weatherService = new WeatherService(mockHttpClient.Object);

            // Act
            var result = await weatherService.GetWeatherAsync(city);

            // Assert
            Assert.IsNull(result);
        }

        private WeatherResponse _GenerateWeatherResponse() 
        { 
            WeatherResponse response = new WeatherResponse()
            {
                coord = new WeatherResponse.Coord()
                {
                    lon = -0.1257,
                    lat = 51.5085
                },
                weather = new List<WeatherResponse.Weather>()
                {
                    new WeatherResponse.Weather()
                    {
                        id = 804,
                        main = "Clouds",
                        description = "overcast clouds",
                        icon = "04d"
                    }
                },
                @base = "stations",
                main = new WeatherResponse.Main()
                {
                    temp = 284.43,
                    feels_like = 284.04,
                    temp_min = 282.86,
                    temp_max = 285.64,
                    pressure = 1026,
                    humidity = 93
                },
                visibility = 10000,
                wind = new WeatherResponse.Wind()
                {
                    speed = 5.14,
                    deg = 250
                },
                clouds = new WeatherResponse.Clouds()
                {
                    all = 100
                },
                dt = 1696403183,
                sys = new WeatherResponse.Sys()
                {
                    type = 2,
                    id = 2006068,
                    country = "GB",
                    sunrise = 1696399512,
                    sunset = 1696440797
                },
                timezone = 3600,
                id = 2643743,
                name = "London",
                cod = 200
            };

            return response;
        }
    }
}
