using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GeographyQuiz.Pages
{
    /// <summary>
    /// A base page for all the pages
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public class BasePage<VM> : Page
        where VM : BaseViewModel, new()
    {
        #region Private Members
        private VM viewModel;
        #endregion

        #region Public Properties
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
        public BasePage()
        {
            ViewModel = new VM();
        }
        #endregion
    }
}
