using System.ComponentModel;

namespace GeographyQuiz
{
    /// <summary>
    /// Represents button values in XAML.
    /// </summary>
    public class Button : INotifyPropertyChanged
    {
        #region Public Properties
        /// <summary>
        /// Content of a button.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// If user was right then the button will change color to green, 
        /// if user was wrong then the button will change color to red.
        /// </summary>
        public string BackgroundColor { get; set; }
        /// <summary>
        /// True if the button contains correct answer.
        /// </summary>
        public bool IsCorrect { get; set; }
        /// <summary>
        /// True if the user clicked this button.
        /// </summary>
        public bool IsSelected { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Button()
        {
            BackgroundColor = "Normal";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
