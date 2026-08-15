using ClimaTempoDesafioAPI.Models;

namespace ClimaTempoDesafioAPI.Repositories.Interfaces
{
    public interface ICidadeFavoritaRepository
    {
        /// Adcionar cidade favorita para o usuário
        Task AdicionarCidadeFavorita(CidadeFavorita cidadeFavorita);

        /// Remover cidade favorita do usuário
        Task RemoverCidadeFavorita(int id, int userId);

        /// Listar cidades favoritas do usuário
        Task<IEnumerable<CidadeFavorita>> ListarCidadesFavoritas(int usuarioId);

        ///Editar cidade favorita do usuário
        Task AtualizarCidadeFavorita(CidadeFavorita cidadeFavorita);

        /// <summary>
        /// Buscar cidade favorita do usuário pelo nome, região e país
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cidadeFavorita"></param>
        /// <returns></returns>
        Task<CidadeFavorita?> BuscarFavorito(int userId, NovaCidadeFavoritaDto cidadeFavorita);
        Task<bool> FavoritoPertenceAoUsuarioAsync(int id, int userId);
    }
}
