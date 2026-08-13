using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Repositories.Interfaces;
using WeatherChallenge.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ClimaTempoDesafioAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorUsernameAsync(string email)
        {
            return await ((IQueryable<Usuario>)_context.Usuario).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Usuario> RegistrarUsuarioAsync(Usuario usuario)
        {
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
