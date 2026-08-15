using ClimaTempoDesafioAPI.Helpers;
using ClimaTempoDesafioAPI.Helpers.Exceptions;
using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Repositories.Interfaces;
using ClimaTempoDesafioAPI.Services.Interfaces;

namespace ClimaTempoDesafioAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Task<Usuario?> ObterPorUsernameAsync(string email)
        {
            email = TratamentoDados.NormalizarEmail(email);
            return _usuarioRepository.ObterPorUsernameAsync(email);
        }

        public async Task<Usuario> RegistrarUsuarioAsync(LoginRequestDto novoUsuariodto)
        {
            Usuario? usuario = await _usuarioRepository.ObterPorUsernameAsync(novoUsuariodto.Email);

            if (usuario is not null)
                throw new BusinessException("E-mail já cadastrado.");

            TratamentoDados.ValidarSenha(novoUsuariodto.Password);

            var novoUsuario = new Usuario
            {
                Email = novoUsuariodto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(novoUsuariodto.Password)
            };

            return await _usuarioRepository.RegistrarUsuarioAsync(novoUsuario);
        }
    }
}
