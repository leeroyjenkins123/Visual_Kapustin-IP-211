using Avalonia.Controls;
using Avalonia.Media;
using program.Controls;
using System;
using Avalonia.Input;

namespace program.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void LogicBufferTappedEventHandler(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (sender is Control control)
        {
            if (control is LogicGate logicGate)
            {
                logicGate.SelectedTextColor = new SolidColorBrush(Colors.Green);
            }
        }
    }
}