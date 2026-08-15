using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Services.Interfaces;
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
            return Ok(await _weatherService.GetForecastDtoAsync(cidade));
        }
    }
}
