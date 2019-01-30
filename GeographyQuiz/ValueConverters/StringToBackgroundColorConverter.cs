using System;
using System.Globalization;
using System.Windows.Media;

namespace GeographyQuiz
    
{
    /// <summary>
    /// Converts string into a SolidColorBrush
    /// </summary>
    public class StringToBackgroundColorConverter : BaseValueConverter<StringToBackgroundColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Initialize new Solid Coor Brush
            SolidColorBrush colorBrush = new SolidColorBrush();

            // Return solid color brush based on the value
            switch ((string)value)
            {
                case "Green":
                    colorBrush.Color = Color.FromRgb(124,252,0);
                    return colorBrush;
                case "Red":
                    colorBrush.Color = Color.FromRgb(240, 128, 128);
                    return colorBrush;
                case "Blue":
                    colorBrush.Color = Color.FromRgb(255,255,255);
                    return colorBrush;
                default:
                    colorBrush.Color = Color.FromRgb(255, 255, 255);
                    return colorBrush;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
