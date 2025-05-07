using System.Globalization;
using Microsoft.Maui.Controls;

namespace WeatherForecastMaui.Converters
{
    /// <summary>
    /// Converte uma string para um valor booleano. Retorna true se a string não for nula ou vazia, caso contrário, false.
    /// </summary>
    public class StringNotEmptyToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Retorna true se a string não for nula ou vazia, false caso contrário.
            return !string.IsNullOrWhiteSpace(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // A conversão de volta não é necessária para este cenário.
            throw new NotImplementedException();
        }
    }
}
