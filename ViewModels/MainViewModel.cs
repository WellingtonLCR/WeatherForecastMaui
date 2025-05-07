using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel; 
using WeatherForecastMaui.Models;
using WeatherForecastMaui.Services;

namespace WeatherForecastMaui.ViewModels
{
    /// <summary>
    /// ViewModel principal para a MainPage. Gerencia o estado da UI e as interações do usuário.
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private readonly WeatherService _weatherService;
        private readonly DatabaseService _databaseService;

        /// <summary>
        /// Nome da cidade inserido pelo usuário.
        /// </summary>
        [ObservableProperty]
        private string city;

        /// <summary>
        /// Data selecionada pelo usuário para a pesquisa de previsão.
        /// Padrão para a data atual.
        /// </summary>
        [ObservableProperty]
        private DateTime selectedDate = DateTime.Now;

        /// <summary>
        /// A previsão do tempo atualmente exibida na UI.
        /// </summary>
        [ObservableProperty]
        private WeatherForecast currentForecast;

        /// <summary>
        /// Indica se uma operação está em andamento (ex: busca de dados).
        /// Usado para controlar o estado de um ActivityIndicator.
        /// </summary>
        [ObservableProperty]
        private bool isBusy;

        /// <summary>
        /// Mensagem de erro a ser exibida na UI, se houver.
        /// </summary>
        [ObservableProperty]
        private string errorMessage;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="MainViewModel"/>.
        /// </summary>
        /// <param name="weatherService">Serviço para buscar dados de previsão da API.</param>
        /// <param name="databaseService">Serviço para interagir com o banco de dados local.</param>
        public MainViewModel(WeatherService weatherService, DatabaseService databaseService)
        {
            _weatherService = weatherService;
            _databaseService = databaseService;
        }

        /// <summary>
        /// Comando executado quando o usuário solicita a busca da previsão do tempo.
        /// </summary>
        [RelayCommand]
        private async Task SearchWeatherAsync()
        {
            if (string.IsNullOrWhiteSpace(City))
            {
                ErrorMessage = "Por favor, insira o nome da cidade";
                CurrentForecast = null; 
                return;
            }

            IsBusy = true;
            ErrorMessage = string.Empty;
            CurrentForecast = null; 

            try
            {
                // Etapa 1: Tenta buscar no banco de dados local primeiro.
                // Isso permite visualizar dados já pesquisados para a cidade e data selecionadas.
                var savedForecasts = await _databaseService.GetForecastsAsync(City, SelectedDate);
                if (savedForecasts.Any())
                {
                    CurrentForecast = savedForecasts.First(); 
                    return; 
                }

                // Etapa 2: Se não encontrado no banco, busca na API.
                // A API (configurada em WeatherService) busca a previsão do tempo *atual* para a cidade.
                var forecastFromApi = await _weatherService.GetWeatherForecastAsync(City);
                
                // Atribui a data selecionada pelo usuário ao objeto de previsão antes de salvar.
                // Isso significa que a previsão *atual* da API será armazenada com a data que o usuário escolheu na UI.
                forecastFromApi.Date = SelectedDate.Date; 

                // Etapa 3: Salva a previsão recém-buscada (e com data ajustada) no banco de dados.
                await _databaseService.SaveForecastAsync(forecastFromApi);
                
                CurrentForecast = forecastFromApi; 
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erro: {ex.Message}";
            }
            finally
            {
                IsBusy = false; 
            }
        }
    }
}
