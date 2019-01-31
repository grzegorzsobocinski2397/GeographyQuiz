using System.Windows.Controls;

namespace GeographyQuiz
{
    /// <summary>
    /// A base page for all the pages. Must have ViewModel which can be new() and is a child of <see cref="BaseViewModel"/>
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public class BasePage<VM> : Page
        where VM : BaseViewModel, new()
    {
        #region Private Members
        /// <summary>
        /// Page view model
        /// </summary>
        private VM viewModel;
        #endregion
        #region Public Properties
        /// <summary>
        /// Page view model
        /// </summary>
        public VM ViewModel
        {
            get { return viewModel; }
            set
            {
                // Checks if anything changed
                if (viewModel == value)
                    return;
                // Update the value if something changed
                viewModel = value;
                // Set the data context for the page
                this.DataContext = viewModel;
                    
            }
        }

        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            ViewModel = new VM();
        }
        #endregion
    }
}
