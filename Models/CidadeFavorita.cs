namespace ClimaTempoDesafioAPI.Models
{
    public class CidadeFavorita
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Posicao { get; set; }
        public bool isExpanded { get; set; } = false;

        public string Localizacao => $"{Name}, {Region}, {Country}";

        public int UserId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        
    }

    public class NovaCidadeFavoritaDto
    {
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }

    public class CidadeFavoritaDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Posicao { get; set; }
        public bool isExpanded { get; set; } = false;
    }

    public class FavoritosDto
    {
        public ICollection<CidadeFavoritaComTempoDto> CidadesFavoritas { get; set; } = new List<CidadeFavoritaComTempoDto>();
    }

    public class CidadeFavoritaComTempoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Posicao { get; set; }
        public bool isExpanded { get; set; } = false;

        public float Temp_c { get; set; }
        public float Humidity { get; set; }
        public float ForecastMaxtemp_c { get; set; }
        public float ForecastMintemp_c { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;

        public ICollection<PrevisaoFavoritoDto> previsao { get; set; } = new List<PrevisaoFavoritoDto>();
    }

    public class PrevisaoFavoritoDto
    {
        public DateTime Data { get; set; }
        public float Humidity { get; set; }
        public float ForecastMaxtemp_c { get; set; }
        public float ForecastMintemp_c { get; set; }

        public string Text { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}
