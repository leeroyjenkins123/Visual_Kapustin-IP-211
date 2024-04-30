using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Globalization;

namespace program.Controls
{
  public class BufferAnsi : LogicGate
  {

    int drawingInput;
    bool Input;
    public BufferAnsi()
    {
      this.ElementName = "";
      Input = true;
    }
    public sealed override void Render(DrawingContext context)
    {
      base.Render(context);

      var elipseRadious = 10;
      var startPointX = 70;
      var startPointY = 70;

      // context.DrawEllipse( // drawing input channel
      //     Brushes.Green,
      //     new Pen(Brushes.Black, 3),
      //     new Avalonia.Point(elipseRadious, startPointY),
      //     elipseRadious,
      //     elipseRadious);


      var Name = new FormattedText("1",
          CultureInfo.CurrentCulture,
          FlowDirection.LeftToRight,
          Typeface.Default,
          TextSize,
          Brushes.Black);

      context.DrawText(Name, new Avalonia.Point(startPointX * 1.2, startPointY * 0.6));

      // context.DrawLine(
      //     new Pen(Brushes.Black, 3),
      //     new Avalonia.Point(2 * elipseRadious, startPointY),
      //     new Avalonia.Point(startPointX, startPointY));

      context.DrawLine(
          new Pen(Brushes.Black, 3),
          new Avalonia.Point(startPointX, startPointY),
          new Avalonia.Point(startPointX, 2 * startPointY)
         );

      context.DrawLine(
          new Pen(Brushes.Black, 3),
          new Avalonia.Point(startPointX, 2 * startPointY),
          new Avalonia.Point(2 * startPointX, startPointY)
         );


      context.DrawLine(
         new Pen(Brushes.Black, 3),
         new Avalonia.Point(2 * startPointX, startPointY),
         new Avalonia.Point(3 * startPointX - elipseRadious, startPointY)
        );

      context.DrawEllipse(
          Brushes.Red,
          new Pen(Brushes.Black, 3),
          new Avalonia.Point(3 * startPointX - elipseRadious, startPointY),
          elipseRadious,
          elipseRadious);




      context.DrawLine(
          new Pen(Brushes.Black, 3),
          new Avalonia.Point(2 * startPointX, startPointY),
          new Avalonia.Point(startPointX, 0));

      context.DrawLine(
          new Pen(Brushes.Black, 3),
          new Avalonia.Point(startPointX, 0),
          new Avalonia.Point(startPointX, startPointY));

      var formattedElementName = new FormattedText(
          ElementName,
          CultureInfo.CurrentCulture,
          FlowDirection.LeftToRight,
          Typeface.Default,
          TextSize,
          SelectedTextColor);

      context.DrawText(formattedElementName, new Avalonia.Point(startPointX + 3, startPointY * 2));

    }
  }
}
