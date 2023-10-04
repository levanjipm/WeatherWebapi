using System;
using System.Linq;
using System.Security.Cryptography;
using WeatherWebapi.Models.Response;
using static WeatherWebapi.Models.Response.WeatherResponse;

namespace WeatherWebapi.Data
{
    public class WeatherData
    {
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public string TimeString { get; set; }
        public string Wind { get; set; }
        public string Visibility { get; set; }
        public string SkyCondition { get; set; }
        public double TemperatureC { get; set; }
        public double TemperatureF { get; set; }
        public double DewPointC { get; set; }
        public double DewPointF { get; set; }
        public int RelativeHumidity { get; set; }
        public int Pressure { get; set; }

        public WeatherData()
        {

        }

        public WeatherData(WeatherResponse response)
        {
            Location = $"{response.name} ({response.coord.lat}, {response.coord.lon})";
            Time = _ConvertTimeIntToDateTime(response.timezone);
            TimeString = _ConvertDateTimeToTimeString(Time);
            Wind = $"{response.wind.speed} m/s, {response.wind.deg} degree";
            Visibility = $"{response.visibility} m";
            SkyCondition = $"{response.weather.FirstOrDefault().main} - {response.weather.FirstOrDefault().description}";
            TemperatureC = _RoundTwoDecimalPlace(response.main.temp - 273.15);
            TemperatureF = _RoundTwoDecimalPlace((TemperatureC * 9 / 4) + 32);
            RelativeHumidity = response.main.humidity;
            DewPointC = _RoundTwoDecimalPlace(TemperatureC - ((100 - RelativeHumidity) / 5));
            DewPointF = _RoundTwoDecimalPlace(TemperatureF - (9 / 25 * (100 - RelativeHumidity)));
            Pressure = response.main.pressure;
        }

        private DateTime _ConvertTimeIntToDateTime(int timeInt)
        {
            DateTime date = DateTime.UtcNow;
            date = date.AddSeconds(timeInt);

            return date;
        }

        private string _ConvertDateTimeToTimeString(DateTime date)
        {
            return date.ToString("dd MMMM yyyy, HH:mm");
        }

        private double _RoundTwoDecimalPlace(double value)
        {
            return Math.Round(value, 2);
        }
    }
}
