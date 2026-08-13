using ClimaTempoDesafioAPI.Helpers;
using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClimaTempoDesafioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public AutenticacaoController(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Register(UsuarioRegistrarDto dto)
        {
            dto.Email = TratamentoDados.NormalizarEmail(dto.Email);

            var usuario = await _usuarioRepository
                .ObterPorUsernameAsync(TratamentoDados.NormalizarEmail(dto.Email));

            if (usuario is not null)
                throw new InvalidOperationException("E-mail já cadastrado.");

            TratamentoDados.ValidarSenha(dto.Password);

            var novoUsuario = new Usuario
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            var registeredUser = await _usuarioRepository.RegistrarUsuarioAsync(novoUsuario);
            return CreatedAtAction(nameof(Register), new { email = registeredUser.Email }, registeredUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("E-mail é obrigatório.");

            var usuario = await _usuarioRepository
                .ObterPorUsernameAsync(TratamentoDados.NormalizarEmail(request.Email));

            if (usuario is null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
                return Unauthorized();

            var token = GenerateToken(usuario);

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

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class UsuarioRegistrarDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
