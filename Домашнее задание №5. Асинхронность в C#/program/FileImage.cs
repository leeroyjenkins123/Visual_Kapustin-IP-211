using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.IO;

namespace program
{
    internal class FileImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var drawingImage = new DrawingImage();

            if (value is DirModel directoryModel)
            {
                var resourse = Application.Current.FindResource("folderIcon");

                if (resourse is DrawingImage image)
                {
                    return image;
                }
            }
            else if (value is ObjModel fileModel)
            {
                var resource = Application.Current.FindResource("fileIcon");

                if (resource is DrawingImage image)
                {
                    return image;
                }
            }

            return drawingImage;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotFiniteNumberException();
        }
    }
}