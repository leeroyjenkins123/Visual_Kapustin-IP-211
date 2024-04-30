using Avalonia.Controls;
using Avalonia.Media;

namespace program.Controls
{
  public abstract class LogicGate : Control
  {
    public int TextSize { get; set; } = 25;

    private int output;
    public int Output
    {
      get => output;
      protected set
      {
        UpdateView();
        output = value;
      }
    }

    private string elementName;
    public string ElementName
    {
      get => elementName;
      protected set => elementName = value;
    }

    private IBrush? selectedTextColor = new SolidColorBrush(Colors.Black);
    public IBrush? SelectedTextColor
    {
      get => selectedTextColor;
      set
      {
        this.UpdateView();
        selectedTextColor = value;
      }
    }

    public void UpdateView()
    {
      this.InvalidateVisual();
    }
  }
}
