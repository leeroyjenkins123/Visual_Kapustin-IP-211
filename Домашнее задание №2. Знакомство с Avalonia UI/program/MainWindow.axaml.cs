using Avalonia.Controls;
using Avalonia.Media;

namespace program;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void ClickHandler(object sender, Avalonia.Interactivity.RoutedEventArgs args)
    {
        if (sender is Button button)
        {
            string color = button.Name.ToString();
            _rect.Fill = Brush.Parse(color.Replace("button", ""));
        }
    }
}