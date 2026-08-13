using ClimaTempoDesafioAPI.Models;

namespace ClimaTempoDesafioAPI.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorUsernameAsync(string email);

        Task<Usuario> RegistrarUsuarioAsync(Usuario usuario);
    }
}
