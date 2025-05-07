using SQLite;

namespace WeatherForecastMaui.Models
{
    /// <summary>
    /// Representa os dados de previsão do tempo.
    /// Esta classe é usada tanto para a desserialização(Envolve a conversão de dados) dos dados da API
    /// quanto para o armazenamento no banco de dados SQLite.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Identificador único para o registro no banco de dados SQLite.
        /// É uma chave primária autoincrementável.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Nome da cidade para a qual a previsão foi obtida.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Data da previsão. Pode ser a data atual da busca ou uma data selecionada pelo usuário.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperatura em graus Celsius.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Descrição textual das condições do tempo (ex: "céu limpo", "chuva moderada").
        /// </summary>
        public string Description { get; set; }
    }
}
