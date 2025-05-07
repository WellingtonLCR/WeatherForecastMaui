using Microsoft.UI.Xaml;

namespace WeatherForecastMaui;

public static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        Microsoft.UI.Xaml.Application.Start(_ => new App());
    }
}
