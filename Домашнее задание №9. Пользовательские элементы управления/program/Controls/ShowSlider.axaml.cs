using Avalonia;
using Avalonia.Controls.Primitives;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive;
using System.Windows.Input;

namespace program
{
    public class ShowSlider : TemplatedControl
    {

        public static readonly StyledProperty<bool> IsSliderVisibleProperty =
           AvaloniaProperty.Register<ShowSlider, bool>("IsSliderVisible");

        public bool IsSliderVisible
        {
            get => GetValue(IsSliderVisibleProperty);
            set
            {
                SetValue(IsSliderVisibleProperty, value);
            }
        }

        public static readonly DirectProperty<ShowSlider, double> SliderWidthProperty =
            AvaloniaProperty.RegisterDirect<ShowSlider, double>(
                nameof(SliderWidth),
                o => o.SliderWidth,
                (o, v) => o.SliderWidth = v);

        private double _sliderWidth = 100;
        public double SliderWidth
        {
            get => _sliderWidth;
            set => SetAndRaise(SliderWidthProperty, ref _sliderWidth, value);
        }

        public static readonly DirectProperty<ShowSlider, double> SliderHeightProperty =
            AvaloniaProperty.RegisterDirect<ShowSlider, double>(
                nameof(SliderHeight),
                o => o.SliderHeight,
                (o, v) => o.SliderHeight = v);

        private double _sliderHeight = 100;
        public double SliderHeight
        {
            get => _sliderHeight;
            set => SetAndRaise(SliderHeightProperty, ref _sliderHeight, value);
        }


        public ReactiveCommand<Unit, Unit> CloseButtonCommand { get; }

        private void CloseButtonAction()
        {
            IsSliderVisible = false;
            SliderWidth = 80;
            SliderHeight = 80;
        }

        public ReactiveCommand<Unit, Unit> OpenSliderCommand { get; }

        private void RectangleTappedAction()
        {
            IsSliderVisible = true;
            SliderWidth = 500;
            SliderHeight = 80;
        }

        public ShowSlider()
        {
            CloseButtonCommand = ReactiveCommand.Create(CloseButtonAction);
            OpenSliderCommand = ReactiveCommand.Create(RectangleTappedAction);
        }
    }
}