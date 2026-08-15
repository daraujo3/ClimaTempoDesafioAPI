using ClimaTempoDesafioAPI.Helpers;
using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Repositories.Interfaces;
using ClimaTempoDesafioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClimaTempoDesafioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;

        public AutenticacaoController(IConfiguration configuration, IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Register(LoginRequestDto dto)
        {
            Usuario usuarioRegistrado = await _usuarioService.RegistrarUsuarioAsync(dto);
            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("E-mail é obrigatório.");

            Usuario? usuario = await _usuarioService
                .ObterPorUsernameAsync(TratamentoDados.NormalizarEmail(request.Email));

            if (usuario is null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
                return Unauthorized();

            string token = GenerateToken(usuario);

            return Ok(new
            {
                token
            });
        }

        private string GenerateToken(Usuario user)
        {
            var key = _configuration["Jwt:Key"];

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(_configuration["Jwt:ExpirationMinutes"]!)
                ),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
