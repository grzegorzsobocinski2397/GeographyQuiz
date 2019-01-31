using System;
using System.Globalization;
using System.Windows;

namespace GeographyQuiz
{
    /// <summary>
    /// Value converter that converts bool into a Visibility value.
    /// </summary>
    public class BooleanToVisibilityConverter : BaseValueConverter<BooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false)
                return Visibility.Hidden;
            else if ((bool)value == true)
                return Visibility.Visible;
            else
                return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
