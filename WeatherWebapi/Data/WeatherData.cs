using System;
using System.Linq;
using WeatherWebapi.Models.Response;

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
            Wind = $"{response.wind.speed} m/s {_DegreesToDirection(response.wind.deg)}";
            Visibility = $"{response.visibility} m";
            SkyCondition = $"{response.weather.FirstOrDefault().main} - {response.weather.FirstOrDefault().description}";
            TemperatureC = _RoundTwoDecimalPlace(response.main.temp - 273.15);
            TemperatureF = _RoundTwoDecimalPlace((TemperatureC * 9 / 4) + 32);
            RelativeHumidity = response.main.humidity;
            DewPointC = _RoundTwoDecimalPlace(TemperatureC - ((100 - RelativeHumidity) / 5));
            DewPointF = _RoundTwoDecimalPlace(TemperatureF - (9 / 25 * (100 - RelativeHumidity)));
            Pressure = response.main.pressure;
        }

        private string _DegreesToDirection(int degrees)
        {
            string[] directions = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
            double degreesPerDirection = 360.0 / directions.Length;
            int index = (int)Math.Round(degrees % 360 / degreesPerDirection);
            if (index < 0)
            {
                index += directions.Length;
            }
            return directions[index];
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
