using Avalonia.Controls;

namespace program;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void DoubleTap(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (sender is ListBox listBox && listBox.DataContext is FileExplorer collection)
        {
            if (e.Source is Control control && control.DataContext is FoldersNFiles file)
            {
                collection.Open(file);
            }
        }
    }

    private void PrevDoubleTap(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        if (sender is ListBox listBox && listBox.DataContext is FileExplorer collection)
        {
            collection.OpenPrev();

        }
    }
}