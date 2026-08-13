using System.Text.Json;

namespace ClimaTempoDesafioAPI.Services
{
    public interface IWeatherService
    {
        Task<JsonDocument> GetCurrentAsync(string query, CancellationToken cancellationToken = default);
        Task<JsonDocument> GetForecastAsync(string query, int? days = null, CancellationToken cancellationToken = default);
    }
}
