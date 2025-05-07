using System.Net.Http.Json;
using WeatherForecastMaui.Models;
using Newtonsoft.Json.Linq;

namespace WeatherForecastMaui.Services
{
    /// <summary>
    /// Responsável por buscar dados de previsão do tempo de uma API externa (OpenWeatherMap).
    /// </summary>
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        // IMPORTANTE: Substitua "YOUR_API_KEY" pela sua chave de API válida do OpenWeatherMap.
        // Você pode obter uma chave gratuita registrando-se em https://openweathermap.org/appid
        private const string API_KEY = "YOUR_API_KEY"; 
        private const string BASE_URL = "https://api.openweathermap.org/data/2.5/weather";

        /// <summary>
        /// Inicializa uma nova instância de <see cref="WeatherService"/>.
        /// </summary>
        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Obtém a previsão do tempo atual para uma cidade especificada a partir da API OpenWeatherMap.
        /// </summary>
        /// <param name="city">O nome da cidade para a qual buscar a previsão.</param>
        /// <returns>Um objeto <see cref="WeatherForecast"/> com os dados da previsão do tempo atual.</returns>
        /// <exception cref="Exception">Lançada se ocorrer um erro durante a busca dos dados ou o parsing da resposta.</exception>
        public async Task<WeatherForecast> GetWeatherForecastAsync(string city)
        {            
            try
            {
                // Monta a URL da requisição com a cidade, API key e unidades (metric para Celsius).
                var requestUrl = $"{BASE_URL}?q={city}&appid={API_KEY}&units=metric&lang=pt_br"; // Adicionado lang=pt_br para descrição em português
                var response = await _httpClient.GetStringAsync(requestUrl);

                // Faz o parsing da resposta JSON.
                var json = JObject.Parse(response);

                // Cria e retorna um objeto WeatherForecast com os dados extraídos.
                // A data é definida como a data atual da máquina, pois esta API retorna o tempo corrente.
                return new WeatherForecast
                {
                    City = city, // A API pode retornar um nome de cidade ligeiramente diferente, usamos o que o usuário digitou para consistência.
                    Date = DateTime.Now.Date, // A API de /weather retorna o tempo atual.
                    Temperature = json["main"]["temp"].Value<double>(),
                    Description = json["weather"][0]["description"].Value<string>()
                };
            }
            catch (HttpRequestException httpEx)
            {
                // Trata erros específicos de HTTP, como cidade não encontrada (404) ou chave de API inválida (401).
                if (httpEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception($"Cidade '{city}' não encontrada. Verifique o nome e tente novamente.", httpEx);
                }
                if (httpEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Chave de API inválida ou não autorizada. Verifique sua API_KEY.", httpEx);
                }
                throw new Exception("Erro de comunicação com o serviço de previsão do tempo.", httpEx);
            }
            catch (Exception ex)
            {                
                // Captura outras exceções (parsing, etc.) e lança uma exceção mais genérica.
                throw new Exception("Erro ao processar os dados da previsão do tempo.", ex);
            }
        }
    }
}
