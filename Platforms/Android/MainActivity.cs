using Android.App;
using Android.Content.PM;
using Android.OS;

namespace WeatherForecastMaui;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
public class MainActivity : MauiAppCompatActivity
{
}
