using ClimaTempoDesafioAPI.Models;

namespace ClimaTempoDesafioAPI.Repositories.Interfaces
{
    public interface ICidadeFavoritaRepository
    {
        /// Adcionar cidade favorita para o usuário
        Task AdicionarCidadeFavorita(CidadeFavorita cidadeFavorita);

        /// Remover cidade favorita do usuário
        Task RemoverCidadeFavorita(int id);

        /// Listar cidades favoritas do usuário
        Task<IEnumerable<CidadeFavorita>> ListarCidadesFavoritas(int usuarioId);

        ///Editar cidade favorita do usuário
        Task AtualizarCidadeFavorita(CidadeFavorita cidadeFavorita);
    }
}
