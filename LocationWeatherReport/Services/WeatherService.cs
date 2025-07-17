using System.Text.Json;
using LocationWeatherReport.Models;

namespace LocationWeatherReport.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "87ec935b0eb34330ec9bff8aee1b256c"; //API Key ID from openweatherMap.org

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherStatus> GetWeatherAsync(string city)
        {
            //Reference to call the API
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            //Store in JSON 
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherStatus>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
