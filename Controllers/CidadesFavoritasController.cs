using ClimaTempoDesafioAPI.Extensions;
using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Repositories.Interfaces;
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
        private readonly ICidadeFavoritaRepository _cidadeFavoritaRepository;

        public CidadesFavoritasController(ICidadeFavoritaRepository cidadeFavoritaRepository)
        {
            _cidadeFavoritaRepository = cidadeFavoritaRepository;
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarCidadeFavorita([FromBody] CidadeFavoritaDto cidadeFavorita)
        {
            await _cidadeFavoritaRepository.AdicionarCidadeFavorita(new CidadeFavorita()
            {
                Name = cidadeFavorita.Name,
                Posicao = cidadeFavorita.Posicao,
                Cor = cidadeFavorita.Cor,
                Destaque = cidadeFavorita.Destaque,
                UserId = User.GetUsuarioId()
            });
            return Created();
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarCidadesFavoritas()
        {
            var usuarioId = User.GetUsuarioId();

            var cidades = await _cidadeFavoritaRepository.ListarCidadesFavoritas(usuarioId);
            return Ok(cidades);
        }

        [HttpDelete("remover")]
        public async Task<IActionResult> RemoverCidadeFavorita([FromQuery] int id)
        {
            await _cidadeFavoritaRepository.RemoverCidadeFavorita(id);
            return Ok();
        }

        [HttpPatch("atualizar")]
        public async Task<IActionResult> AtualizarCidadeFavorita([FromBody] CidadeFavorita cidadeFavorita)
        {
            await _cidadeFavoritaRepository.AtualizarCidadeFavorita(cidadeFavorita);
            return Ok();
        }
    }
}
