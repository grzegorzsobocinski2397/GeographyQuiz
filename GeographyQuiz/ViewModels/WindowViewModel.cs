using System.Windows;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class WindowViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// The window for this view model
        /// </summary>
        private Window window;
        /// <summary>
        /// Radius of the edges
        /// </summary>
        private readonly int windowCorner = 10;
        /// <summary>
        /// Outer margin for the drop shadow
        /// </summary>
        private readonly int outerMargin = 10;
        #endregion
        #region Public Properties
        /// <summary>
        /// Outer margin for the drop shadow
        /// </summary>
        public Thickness OuterMarginThickness => new Thickness(outerMargin);
        /// <summary>
        /// Radius of the edges
        /// </summary>
        public CornerRadius WindowCornerRadius => new CornerRadius(windowCorner);
        /// <summary>
        /// Current page being displayed in the application.
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } 
        #endregion
        #region Commands
        /// <summary>
        /// Closes the application
        /// </summary>
        public ICommand CloseWindowCommand { get; set; }
        #endregion
        #region Constructor
        public WindowViewModel(Window window)
        {
            // Binds the window to the view model
            this.window = window;

            // Creates commands
            CloseWindowCommand = new RelayCommand(() => window.Close());

            // Sets the first page as ChooseGamePage
            CurrentPage = ApplicationPage.ChooseGamePage;
        }
        #endregion
    }
}
