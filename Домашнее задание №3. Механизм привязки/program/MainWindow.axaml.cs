using Avalonia.Controls;
using Avalonia.Media;
using System;
using Program;
using Avalonia.Interactivity;

namespace program;

public partial class MainWindow : Window
{
    Calculator calc = new Calculator();
    public MainWindow()
    {
        InitializeComponent();
    }
}