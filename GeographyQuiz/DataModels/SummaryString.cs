

using System.Windows.Media;

namespace GeographyQuiz
{
    /// <summary>
    /// Class for the summary string that is displayed on the <see cref="SummaryPage"/>
    /// </summary>
    public class SummaryString
    {
        #region Public Properties
        /// <summary>
        /// Tells the user if he was right or wrong.
        /// </summary>
        public string SummaryText { get; set; }
        /// <summary>
        /// Green is user was right and red if user was wrong.
        /// </summary>
        public SolidColorBrush SummaryTextColor { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor for the <see cref="SummaryString"/>.
        /// </summary>
        /// <param name="summaryText">Tells the user if he was right or wrong.</param>
        /// <param name="summaryTextColor">"Green" is user was right and "red" if user was wrong.</param>
        public SummaryString(string summaryText, string summaryTextColor)
        {
            SummaryText = summaryText;

            // Sets the string color accroding to the answer
            SolidColorBrush scb = new SolidColorBrush();
            if(summaryTextColor == "Green")
            {
                scb.Color = Color.FromRgb(0, 130, 0);
                SummaryTextColor = scb;
            }
            else if (summaryTextColor == "Red")
            {
                scb.Color = Color.FromRgb(255, 100, 100);
                SummaryTextColor = scb;
            }
        }
        #endregion

    }
}
