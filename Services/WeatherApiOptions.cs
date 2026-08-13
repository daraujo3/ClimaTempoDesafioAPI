namespace ClimaTempoDesafioAPI.Services
{
    public class WeatherApiOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public int DefaultForecastDays { get; set; } = 5;
        public string BaseUrl { get; set; } = "https://api.weatherapi.com/";
    }
}