namespace ClimaTempoDesafioAPI.Models
{
    public class CidadeFavorita
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Posicao { get; set; }
        public string Cor { get; set; } = string.Empty;
        public string Destaque { get; set; } = string.Empty;

        public int UserId { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }

    public class CidadeFavoritaDto
    {
        public string Name { get; set; } = string.Empty;
        public int Posicao { get; set; }
        public string Cor { get; set; } = string.Empty;
        public string Destaque { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
