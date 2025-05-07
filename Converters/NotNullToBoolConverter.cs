using System.Globalization;
using Microsoft.Maui.Controls;

namespace WeatherForecastMaui.Converters
{
    /// <summary>
    /// Converte um objeto para um valor booleano. Retorna true se o objeto não for nulo, caso contrário, false.
    /// </summary>
    public class NotNullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Retorna true se o valor não for nulo, false caso contrário.
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // A conversão de volta não é necessária para este cenário.
            throw new NotImplementedException();
        }
    }
}
