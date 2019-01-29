using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class ChooseGameViewModel : BaseViewModel
    {
        #region Commands
        /// <summary>
        /// Starts new capitals game
        /// </summary>
        public ICommand CapitalsCommand { get; set; }
        /// <summary>
        /// Starts new flags game
        /// </summary>
        public ICommand FlagsCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public ChooseGameViewModel()
        {
            // Creates commands
            CapitalsCommand = new RelayCommand(() => ChangePage(ApplicationPage.DifficultyPage));
           
        }
        #endregion
       
    }
}
