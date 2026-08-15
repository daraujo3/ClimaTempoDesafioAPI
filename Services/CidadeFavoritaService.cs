using ClimaTempoDesafioAPI.Helpers.Exceptions;
using ClimaTempoDesafioAPI.Models;
using ClimaTempoDesafioAPI.Repositories;
using ClimaTempoDesafioAPI.Repositories.Interfaces;
using ClimaTempoDesafioAPI.Services.Interfaces;

namespace ClimaTempoDesafioAPI.Services
{
    public class CidadeFavoritaService : ICidadeFavoritaService
    {
        private readonly IWeatherService _weatherService;
        private readonly ICidadeFavoritaRepository _cidadeFavoritaRepository;

        public CidadeFavoritaService(
            ICidadeFavoritaRepository cidadeFavoritaRepository,
            IWeatherService weatherService
            )
        {
            _cidadeFavoritaRepository = cidadeFavoritaRepository;
            _weatherService = weatherService;
        }

        public async Task AdicionarCidadeFavorita(int userId, NovaCidadeFavoritaDto cidadeFavorita)
        {
            CidadeFavorita? cidadeFavoritaJaExiste = await _cidadeFavoritaRepository.BuscarFavorito(userId, cidadeFavorita);

            if (cidadeFavoritaJaExiste != null)
            {
                throw new BusinessException("Cidade já adicionada aos favoritos.");
            }

            await _cidadeFavoritaRepository.AdicionarCidadeFavorita(new CidadeFavorita() {
                UserId = userId,
                Name = cidadeFavorita.Name,
                Region = cidadeFavorita.Region,
                Country = cidadeFavorita.Country,
                isExpanded = false,
                Posicao = 0
            });
        }

        public async Task AtualizarCidadeFavorita(int userId, ICollection<CidadeFavoritaDto> cidadesFavoritas)
        {
            if (cidadesFavoritas == null || cidadesFavoritas.Count == 0)
                return;

            var _cidadesFavoritas = await _cidadeFavoritaRepository.ListarCidadesFavoritas(userId);

            foreach (var cidade in cidadesFavoritas)
            {
                var cidadeExistente = _cidadesFavoritas.FirstOrDefault(c => c.Id == cidade.Id);

                if (cidadeExistente == null)
                    continue;

                cidadeExistente.isExpanded = cidade.isExpanded;
                cidadeExistente.Posicao = cidade.Posicao;
            }

            await _cidadeFavoritaRepository.AtualizarCidadesFavorita(_cidadesFavoritas);
        }

        public async Task<FavoritosDto> ListarCidadesFavoritas(int userId)
        {
            FavoritosDto dto = new FavoritosDto();
            var _cidadesFavoritas = await _cidadeFavoritaRepository.ListarCidadesFavoritas(userId);
            foreach (var cidade in _cidadesFavoritas)
            {
                var weather = await _weatherService.GetForecastAsync(cidade.Localizacao);
                dto.CidadesFavoritas.Add(new CidadeFavoritaComTempoDto()
                {
                    Id = cidade.Id,
                    Name = cidade.Name,
                    Region = cidade.Region,
                    Country = cidade.Country,
                    Posicao = cidade.Posicao,
                    isExpanded = cidade.isExpanded,
                    Temp_c = weather.current.temp_c,
                    Humidity = weather.current.humidity,
                    ForecastMaxtemp_c = weather.forecast.forecastday[0].day.maxtemp_c,
                    ForecastMintemp_c = weather.forecast.forecastday[0].day.mintemp_c,
                    Text = weather.current.condition.text,
                    Icon = weather.current.condition.icon,
                    previsao = weather.forecast.forecastday.Select(f => new PrevisaoFavoritoDto()
                    {
                        Data = DateTime.TryParseExact(f.date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var parsedDate) ? parsedDate : default,
                        ForecastMaxtemp_c = f.day.maxtemp_c,
                        ForecastMintemp_c = f.day.mintemp_c,
                        Text = f.day.condition.text,
                        Icon = f.day.condition.icon
                    }).ToList()
                });
            }

            return dto;
        }

        public async Task RemoverCidadeFavorita(int id, int userId)
        {
            if (!await _cidadeFavoritaRepository.FavoritoPertenceAoUsuarioAsync(id, userId))
            {
                throw new BusinessException("Não delete os favoritos dos outros");
            }

            await _cidadeFavoritaRepository.RemoverCidadeFavorita(id, userId);
        }
    }
}
