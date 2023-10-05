using System.Threading.Tasks;

namespace WeatherWebapi.Services.HttpClientWrapper
{
    public interface IHttpClientWrapper
    {
        Task<string> GetStringAsync(string requestUri);
    }
}
