using ClimaTempoDesafioAPI.Helpers.Extensions;
using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Repositories.Interfaces;
using ClimaTempoDesafioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClimaTempoDesafioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CidadesFavoritasController : ControllerBase
    {
        private readonly ICidadeFavoritaService _cidadeFavoritaService;

        public CidadesFavoritasController(ICidadeFavoritaService cidadeFavoritaService)
        {
            _cidadeFavoritaService = cidadeFavoritaService;
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarCidadeFavorita([FromBody] NovaCidadeFavoritaDto cidadeFavorita)
        {
            await _cidadeFavoritaService.AdicionarCidadeFavorita(User.GetUsuarioId(), cidadeFavorita);
            return Created();
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarCidadesFavoritas()
        {
            var cidades = await _cidadeFavoritaService.ListarCidadesFavoritas(User.GetUsuarioId());
            return Ok(cidades);
        }

        [HttpDelete("remover")]
        public async Task<IActionResult> RemoverCidadeFavorita([FromQuery] int id)
        {
            await _cidadeFavoritaService.RemoverCidadeFavorita(id, User.GetUsuarioId());
            return Ok();
        }

        [HttpPatch("atualizar")]
        public async Task<IActionResult> AtualizarCidadeFavorita([FromBody] ICollection<CidadeFavoritaDto> cidadesFavoritas)
        {
            await _cidadeFavoritaService.AtualizarCidadeFavorita(cidadesFavoritas);
            return Ok();
        }
    }
}
