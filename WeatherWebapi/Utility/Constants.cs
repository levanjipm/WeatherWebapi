namespace WeatherWebapi.Utility
{
    public class Constants
    {
        public const string WEATHER_API_URL = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}";
        // For real development, it is best to store key or other confidential value in some kind of "Parameter Store" not in plain string
        public const string API_KEY = "b0b4f3cecfe622820a33d2ff7faceadd";
    }
}
