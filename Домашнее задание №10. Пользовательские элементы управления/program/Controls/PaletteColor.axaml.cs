using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia.Interactivity;
using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.Shapes;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace program.Controls
{
    public partial class PaletteColor : UserControl
    {
        private int c = 0;
        public static readonly StyledProperty<Color> SelectedColorProperty =
            AvaloniaProperty.Register<PaletteColor, Color>(nameof(SelectedColor));

        public Color SelectedColor
        {
            get => GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public static readonly StyledProperty<ObservableCollection<Color>> ColorDictionaryProperty =
            AvaloniaProperty.Register<PaletteColor, ObservableCollection<Color>>(nameof(ColorDictionary));

        public ObservableCollection<Color> ColorDictionary
        {
            get => GetValue(ColorDictionaryProperty);
            set => SetValue(ColorDictionaryProperty, value);
        }

        public static readonly StyledProperty<ObservableCollection<Color>> AdditionalColorsProperty =
           AvaloniaProperty.Register<PaletteColor, ObservableCollection<Color>>(nameof(AdditionalColors));

        public ObservableCollection<Color> AdditionalColors
        {
            get => GetValue(AdditionalColorsProperty);
            set => SetValue(AdditionalColorsProperty, value);
        }

        public static readonly StyledProperty<double> HueProperty =
            AvaloniaProperty.Register<PaletteColor, double>(nameof(Hue));

        public double Hue
        {
            get => GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }

        public static readonly StyledProperty<double> ContrastProperty =
            AvaloniaProperty.Register<PaletteColor, double>(nameof(Contrast));

        public double Contrast
        {
            get => GetValue(ContrastProperty);
            set => SetValue(ContrastProperty, value);
        }

        public static readonly StyledProperty<double> BrightnessProperty =
            AvaloniaProperty.Register<PaletteColor, double>(nameof(Brightness));

        public double Brightness
        {
            get => GetValue(BrightnessProperty);
            set => SetValue(BrightnessProperty, value);
        }

        public static readonly StyledProperty<double> RedProperty =
            AvaloniaProperty.Register<PaletteColor, double>(nameof(Red));

        public double Red
        {
            get => GetValue(RedProperty);
            set => SetValue(RedProperty, value);
        }

        public static readonly StyledProperty<double> GreenProperty =
            AvaloniaProperty.Register<PaletteColor, double>(nameof(Green));

        public double Green
        {
            get => GetValue(GreenProperty);
            set => SetValue(GreenProperty, value);
        }

        public static readonly StyledProperty<double> BlueProperty =
            AvaloniaProperty.Register<PaletteColor, double>(nameof(Blue));

        public double Blue
        {
            get => GetValue(BlueProperty);
            set => SetValue(BlueProperty, value);
        }

        public PaletteColor()
        {
            InitializeComponent();
            DataContext = this;
            colorSpectrum.ColorChanged += ColorSpectrum_ColorChanged;
            ColorDictionary = new ObservableCollection<Color>
            {
                Colors.Red,
                Colors.Green,
                Colors.Blue,
                Colors.White,
                Colors.Black,
                Colors.Yellow,
                Colors.Orange,
                Colors.Purple,
                Colors.Pink,
                Colors.Brown,
                Colors.Cyan,
                Colors.Magenta,
                Colors.LightGray,
                Colors.DarkGray,
                Colors.Azure,
                Colors.AliceBlue,
                Colors.Aqua,
                Colors.Aquamarine,
                Colors.Beige,
                Colors.Bisque,
                Colors.Coral,
                Colors.CornflowerBlue,
                Colors.DarkBlue,
                Colors.DarkCyan,
                Colors.DarkGreen,
                Colors.DarkMagenta,
                Colors.DarkOrange,
                Colors.DarkRed,
                Colors.DeepPink,
                Colors.DeepSkyBlue,
                Colors.ForestGreen,
                Colors.Gold,
                Colors.Gray,
                Colors.IndianRed,
                Colors.Indigo,
                Colors.Khaki,
                Colors.Lavender,
                Colors.Lime,
                Colors.Maroon,
                Colors.Olive,
                Colors.Orchid,
                Colors.PowderBlue,
                Colors.RosyBrown,
                Colors.SaddleBrown,
                Colors.Silver,
                Colors.Teal,
                Colors.Turquoise,
                Colors.Violet
            };
            AdditionalColors = new ObservableCollection<Color>
            {
                Colors.White, Colors.White, Colors.White, Colors.White,
                Colors.White, Colors.White, Colors.White, Colors.White,
                Colors.White, Colors.White, Colors.White, Colors.White,
                Colors.White, Colors.White, Colors.White, Colors.White,
                Colors.White, Colors.White, Colors.White, Colors.White,
                Colors.White, Colors.White, Colors.White, Colors.White,
                Colors.White, Colors.White, Colors.White, Colors.White,
                Colors.White, Colors.White, Colors.White, Colors.White
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            colorSpectrum = this.FindControl<ColorSpectrum>("colorSpectrum");
            // colorSlider = this.FindControl<ColorSlider>("colorSlider");
        }

        private Color selectedSquareColor;


        private void Rectangle_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (sender is Rectangle rectangle && rectangle.Fill is SolidColorBrush brush)
            {
                SelectedColor = brush.Color;
                selectedSquareColor = brush.Color; // Сохраняем выбранный цвет
                colorSpectrum.Color = brush.Color;
                // colorSlider.Color = brush.Color;
                UpdateColorProperties(brush.Color);
            }
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (c == 0)
            {
                c = 1;
            }


        }




        private void AdditionalColorRectangle_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (sender is Rectangle rectangle && rectangle.Fill is SolidColorBrush brush)
            {
                var index = AdditionalColors.IndexOf(brush.Color);
                Console.WriteLine($"index:{index}");
                if (index != -1 && c == 1)
                {
                    AdditionalColors[index] = SelectedColor;
                    c = 0;
                }
                var color = brush.Color;
                SelectedColor = color;
                colorSpectrum.Color = color;
                // colorSlider.Color = color;
                UpdateColorProperties(color);
            }
        }


        private void ColorSpectrum_ColorChanged(object sender, ColorChangedEventArgs e)
        {
            if (sender is ColorSpectrum spectrum)
            {
                SelectedColor = spectrum.Color;
                // colorSlider.Color = spectrum.Color;
                UpdateColorProperties(spectrum.Color);

            }
        }

        private void UpdateColorProperties(Color color)
        {
            var hslColor = color.RgbToHsl();
            var hue = hslColor.H;
            var contrast = CalculateContrast(color);
            var brightness = hslColor.L;

            var red = color.R;
            var green = color.G;
            var blue = color.B;

            SetValue(HueProperty, hue);
            SetValue(ContrastProperty, contrast);
            SetValue(BrightnessProperty, brightness);
            SetValue(RedProperty, red);
            SetValue(GreenProperty, green);
            SetValue(BlueProperty, blue);
        }

        private double CalculateContrast(Color color)
        {
            var luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            return luminance < 0.5 ? 0 : 255;
        }
    }

    public static class ColorExtensions
    {
        public static HslColor RgbToHsl(this Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            double h, s, l = (max + min) / 2;

            if (max == min)
            {
                h = s = 0;
            }
            else
            {
                double d = max - min;
                s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
                if (max == r)
                    h = (g - b) / d + (g < b ? 6 : 0);
                else if (max == g)
                    h = (b - r) / d + 2;
                else
                    h = (r - g) / d + 4;
                h /= 6;
            }

            return new HslColor(h * 360, s, l, 100);
        }

        public static Color HslToRgb(this HslColor hsl)
        {
            double r, g, b;

            if (hsl.S == 0)
            {
                r = g = b = hsl.L;
            }
            else
            {
                double q = hsl.L < 0.5 ? hsl.L * (1 + hsl.S) : hsl.L + hsl.S - hsl.L * hsl.S;
                double p = 2 * hsl.L - q;
                r = HueToRgb(p, q, hsl.H + 1.0 / 3);
                g = HueToRgb(p, q, hsl.H);
                b = HueToRgb(p, q, hsl.H - 1.0 / 3);
            }

            return Color.FromArgb(255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

        private static double HueToRgb(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1.0 / 6) return p + (q - p) * 6 * t;
            if (t < 1.0 / 2) return q;
            if (t < 2.0 / 3) return p + (q - p) * (2.0 / 3 - t) * 6;
            return p;
        }
    }

    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return new SolidColorBrush(color);
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                return brush.Color;
            }

            return Colors.Transparent;
        }
    }
}
