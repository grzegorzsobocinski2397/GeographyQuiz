using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GeographyQuiz
{
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
      

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.GamePage:
                    return new GamePage();
                case ApplicationPage.DifficultyPage:
                    return new DifficultyPage();
                case ApplicationPage.CapitalsGame:
                    return new CapitalsGamePage();
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
