using Avalonia.Controls;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace program;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void GetForecastButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is WeatherLogic weatherModel)
        {
            if (InputForm.Text != string.Empty)
            {
                try
                {
                    weatherModel.Forecast(InputForm.Text);
                    // FindCity.Clear();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}