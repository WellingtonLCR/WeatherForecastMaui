using SQLite;
using WeatherForecastMaui.Models;
using Microsoft.Maui.Storage;

namespace WeatherForecastMaui.Services
{
    /// <summary>
    /// Gerencia as operações de banco de dados SQLite para armazenar e recuperar previsões do tempo.
    /// </summary>
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="DatabaseService"/>.
        /// Configura a conexão com o banco de dados e cria a tabela WeatherForecast se ela não existir.
        /// </summary>
        public DatabaseService()
        {
            // Define o caminho para o arquivo do banco de dados no diretório de dados do aplicativo.
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "weather.db");
            _database = new SQLiteAsyncConnection(dbPath);
            // Cria a tabela WeatherForecast de forma assíncrona. Wait() é usado aqui porque 
            // o construtor não pode ser assíncrono, mas a criação da tabela precisa ser garantida.
            _database.CreateTableAsync<WeatherForecast>().Wait();
        }

        /// <summary>
        /// Obtém as previsões do tempo armazenadas para uma cidade e data específicas.
        /// </summary>
        /// <param name="city">O nome da cidade.</param>
        /// <param name="date">A data da previsão.</param>
        /// <returns>Uma lista de <see cref="WeatherForecast"/> correspondente aos critérios de busca.</returns>
        public async Task<List<WeatherForecast>> GetForecastsAsync(string city, DateTime date)
        {
            // Busca na tabela WeatherForecast onde a cidade e a data (ignorando a hora) correspondem.
            return await _database.Table<WeatherForecast>()
                .Where(w => w.City == city && w.Date.Date == date.Date)
                .ToListAsync();
        }

        /// <summary>
        /// Salva uma nova previsão do tempo no banco de dados.
        /// </summary>
        /// <param name="forecast">O objeto <see cref="WeatherForecast"/> a ser salvo.</param>
        /// <returns>O número de linhas inseridas no banco de dados (geralmente 1 se bem-sucedido).</returns>
        public async Task<int> SaveForecastAsync(WeatherForecast forecast)
        {
            // Insere o objeto forecast na tabela.
            return await _database.InsertAsync(forecast);
        }
    }
}
