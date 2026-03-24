using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace CulinaryGuide.Converters
{
    public class FavoriteColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool isFavorite && isFavorite) ? Colors.LightCoral : Colors.LightBlue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}