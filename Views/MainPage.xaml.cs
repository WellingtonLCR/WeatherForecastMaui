using WeatherForecastMaui.ViewModels;

namespace WeatherForecastMaui.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
