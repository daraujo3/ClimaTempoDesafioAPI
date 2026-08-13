namespace ClimaTempoDesafioAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<CidadeFavorita> CidadesFavoritas { get; set; } = new List<CidadeFavorita>();
    }
}
