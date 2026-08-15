using ClimaTempoDesafioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WeatherChallenge.Api.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuario => Set<Usuario>();
    public DbSet<CidadeFavorita> CidadeFavorita => Set<CidadeFavorita>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Email).HasMaxLength(200).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.PasswordHash).HasMaxLength(500).IsRequired();
        });

        modelBuilder.Entity<CidadeFavorita>(entity =>
        {
            entity.ToTable("CidadesFavoritas");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Region).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Country).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Cor).HasMaxLength(50);
            entity.Property(x => x.Tamanho).HasMaxLength(50);
            entity.Property(x => x.Posicao);
            
            entity.HasOne(x => x.Usuario)
                  .WithMany(x => x.CidadesFavoritas)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(x => new { x.UserId, x.Name, x.Region, x.Country }).IsUnique();
        });
    }
}
