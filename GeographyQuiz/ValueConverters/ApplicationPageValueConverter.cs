using System;
using System.Diagnostics;
using System.Globalization;

namespace GeographyQuiz
{
    /// <summary>
    /// Converts <see cref="ApplicationPage"/> into a Page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.ChooseGamePage:
                    return new ChooseGamePage();
                case ApplicationPage.DifficultyPage:
                    return new DifficultyPage();
                case ApplicationPage.GamePage:
                    return new GamePage();
                case ApplicationPage.SummaryPage:
                    return new SummaryPage();
                default:
                    Debugger.Break();
                    return null;
            };
        }

       
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
