using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using WeatherChallenge.Api.Data;

namespace ClimaTempoDesafioAPI.Repositories
{
    public class CidadeFavoritaRepository : ICidadeFavoritaRepository
    {
        private readonly AppDbContext _context;

        public CidadeFavoritaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarCidadeFavorita(CidadeFavorita cidadeFavorita)
        {
            await _context.CidadeFavorita.AddAsync(cidadeFavorita);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarCidadeFavorita(CidadeFavorita cidadeFavorita)
        {
            var cidade = await _context.CidadeFavorita.FirstOrDefaultAsync(a => a.Id == cidadeFavorita.Id);
            if (cidade != null)
            {
                _context.Entry(cidade).CurrentValues.SetValues(cidadeFavorita);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CidadeFavorita?> BuscarFavorito(int userId, NovaCidadeFavoritaDto cidadeFavorita)
        {
            return await _context.CidadeFavorita.FirstOrDefaultAsync(c => 
            c.UserId == userId 
            && c.Name == cidadeFavorita.Name 
            && c.Region == cidadeFavorita.Region 
            && c.Country == cidadeFavorita.Country);
        }

        public async Task<bool> FavoritoPertenceAoUsuarioAsync(int id, int userId)
        {
            var favorito = await _context.CidadeFavorita.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            return favorito != null;
        }

        public async Task<IEnumerable<CidadeFavorita>> ListarCidadesFavoritas(int usuarioId)
        {
            return await _context.CidadeFavorita.Where(c => c.UserId == usuarioId).ToListAsync();
        }

        public async Task RemoverCidadeFavorita(int id, int userId)
        {
            var cidade = await _context.CidadeFavorita.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (cidade != null)
            {
                _context.CidadeFavorita.Remove(cidade);
                await _context.SaveChangesAsync();
            }
        }
    }
}
