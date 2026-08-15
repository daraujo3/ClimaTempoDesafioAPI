using ClimaTempoDesafioAPI.Models;
using System.Text.Json;

namespace ClimaTempoDesafioAPI.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<JsonDocument> GetCurrentAsync(string query, CancellationToken cancellationToken = default);
        Task<ReponseAPIWeather> GetForecastAsync(string query, int? days = null, CancellationToken cancellationToken = default);
        Task<CidadeFavoritaComTempoDto> GetForecastDtoAsync(string cidade);
    }
}
