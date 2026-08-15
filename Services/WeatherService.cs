using ClimaTempoDesafioAPI.Helpers.Exceptions;
using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ClimaTempoDesafioAPI.Services
{
    /// <summary>
    /// Represents a service for interacting with the Weather API.
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherApiOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherService"/> class with the specified HTTP client and options.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WeatherService(HttpClient httpClient, IOptions<WeatherApiOptions> options)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

            // Ensure base address is set on the client if provided in options
            if (!string.IsNullOrWhiteSpace(_options.BaseUrl) && _httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_options.BaseUrl);
            }
        }

        /// <summary>
        /// Gets the current weather for a given location.
        /// </summary>
        /// <param name="query">The location for which to get current weather.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the JSON document with the current weather information.</returns>
        /// <exception cref="ArgumentException">Thrown when the query is null or whitespace.</exception>
        public async Task<JsonDocument> GetCurrentAsync(string query, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("Termo de busca é obrigatório", nameof(query));

            var url = $"v1/current.json?key={Uri.EscapeDataString(_options.ApiKey)}&q={Uri.EscapeDataString(query)}";
            var resp = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
            var stream = await resp.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            return await JsonDocument.ParseAsync(stream, default, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the weather forecast for a given location and number of days.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="days">The number of days for which to get the forecast.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the JSON document with the weather forecast information.</returns>
        /// <exception cref="ArgumentException">Thrown when the query is null or whitespace.</exception>
        public async Task<ReponseAPIWeather> GetForecastAsync(string query, int? days = null, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("Termo de busca é obrigatório", nameof(query));

                var useDays = days ?? _options.DefaultForecastDays;
                if (useDays < 1) useDays = 1;
                if (useDays > 10) useDays = 10;

                var url = $"v1/forecast.json?key={Uri.EscapeDataString(_options.ApiKey)}&q={Uri.EscapeDataString(query)}&days={useDays}";
                var resp = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
                resp.EnsureSuccessStatusCode();
                var stream = await resp.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                var jsonDocument = await JsonDocument.ParseAsync(stream, default, cancellationToken).ConfigureAwait(false);
                return JsonSerializer.Deserialize<ReponseAPIWeather>(jsonDocument.RootElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception)
            {
                throw new BusinessException("Não encontrado dados");
            }
        }

        public async Task<CidadeFavoritaComTempoDto> GetForecastDtoAsync(string cidade)
        {
            var weather = await GetForecastAsync(cidade);
            return new CidadeFavoritaComTempoDto()
            {
                Name = weather.location.name,
                Region = weather.location.region,
                Country = weather.location.country,
                Temp_c = weather.current.temp_c,
                Humidity = weather.current.humidity,
                ForecastMaxtemp_c = weather.forecast.forecastday[0].day.maxtemp_c,
                ForecastMintemp_c = weather.forecast.forecastday[0].day.mintemp_c,
                Text = weather.current.condition.text,
                Icon = weather.current.condition.icon,
                previsao = weather.forecast.forecastday.Select(f => new PrevisaoFavoritoDto()
                {
                    Data = DateTime.TryParseExact(f.date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var parsedDate) ? parsedDate : default,
                    ForecastMaxtemp_c = f.day.maxtemp_c,
                    ForecastMintemp_c = f.day.mintemp_c,
                    Text = f.day.condition.text,
                    Icon = f.day.condition.icon
                }).ToList()
            };
        }
    }
}
