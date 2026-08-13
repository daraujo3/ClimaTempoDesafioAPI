using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ClimaTempoDesafioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PrevisaoTempoController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public PrevisaoTempoController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet(Name = "GetPrevisaoDoTempo")]
        public async Task<ActionResult<ClimaTempo>> Get([FromQuery] string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade)) return BadRequest("cidade is required");

            try
            {
                using var doc = await _weatherService.GetForecastAsync(cidade);
                var root = doc.RootElement;

                var clima = new ClimaTempo();

                if (root.TryGetProperty("location", out var location))
                {
                    clima.Name = location.GetProperty("name").GetString() ?? string.Empty;
                    clima.Region = location.GetProperty("region").GetString() ?? string.Empty;
                    clima.Country = location.GetProperty("country").GetString() ?? string.Empty;
                }

                if (root.TryGetProperty("current", out var current))
                {
                    if (current.TryGetProperty("temp_c", out var temp)) clima.Temp_c = (float)temp.GetDouble();
                    if (current.TryGetProperty("humidity", out var hum)) clima.Humidity = (float)hum.GetDouble();
                    if (current.TryGetProperty("condition", out var cond))
                    {
                        clima.Condition = new Condition
                        {
                            Text = cond.GetProperty("text").GetString() ?? string.Empty,
                            Icon = cond.GetProperty("icon").GetString() ?? string.Empty,
                            Code = cond.GetProperty("code").GetInt32()
                        };
                    }
                }

                if (root.TryGetProperty("forecast", out var forecast) &&
                    forecast.TryGetProperty("forecastday", out var daysArr) &&
                    daysArr.ValueKind == JsonValueKind.Array &&
                    daysArr.GetArrayLength() > 0)
                {
                    var firstDay = daysArr[0].GetProperty("day");
                    if (firstDay.TryGetProperty("maxtemp_c", out var max)) clima.ForecastMaxtemp_c = (float)max.GetDouble();
                    if (firstDay.TryGetProperty("mintemp_c", out var min)) clima.ForecastMintemp_c = (float)min.GetDouble();
                }

                return Ok(clima);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
            }
            catch (JsonException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
