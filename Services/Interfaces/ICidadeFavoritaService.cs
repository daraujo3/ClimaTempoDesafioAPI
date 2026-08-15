using ClimaTempoDesafioAPI.Models;

namespace ClimaTempoDesafioAPI.Services.Interfaces
{
    public interface ICidadeFavoritaService
    {
        Task AdicionarCidadeFavorita(int userId, NovaCidadeFavoritaDto cidadeFavorita);
        Task AtualizarCidadeFavorita(ICollection<CidadeFavoritaDto> cidadesFavoritas);
        Task<FavoritosDto> ListarCidadesFavoritas(int userId);
        Task RemoverCidadeFavorita(int id, int userId);
    }
}
