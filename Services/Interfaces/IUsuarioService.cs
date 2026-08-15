using ClimaTempoDesafioAPI.Models;

namespace ClimaTempoDesafioAPI.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> ObterPorUsernameAsync(string email);
        Task<Usuario> RegistrarUsuarioAsync(LoginRequestDto novoUsuario);
    }
}
