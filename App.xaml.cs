using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Application = Microsoft.Maui.Controls.Application;
using WeatherForecastMaui.Views;

namespace WeatherForecastMaui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(
            new MainPage(
                new ViewModels.MainViewModel(
                    new Services.WeatherService(),
                    new Services.DatabaseService()
                )
            )
        );
    }
}
