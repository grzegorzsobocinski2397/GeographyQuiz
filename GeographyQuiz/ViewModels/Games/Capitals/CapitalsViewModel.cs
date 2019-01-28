using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class CapitalsViewModel : BaseViewModel
    {

        #region Public Properties
        /// <summary>
        /// Difficulty level chosen by the user
        /// </summary>
        public int DifficultyLevel { get; set; }
        public bool IsDifficultyUserControlVisible { get; set; } = true;
        #endregion
        #region Commands
        /// <summary>
        /// Difficulty level clicked by the user
        /// </summary>
        public ICommand DifficultyCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public CapitalsViewModel()
        {   

            // Creates commands
            DifficultyCommand = new RelayParameterCommand(async (parameter) => StartGame(parameter));
        }
        #endregion
        #region Private Methods
        private void StartGame(object parameter)
        {
            // Changes the difficulty level
            DifficultyLevel = int.Parse(parameter as string);

            // Hides the Difficulty user control
            IsDifficultyUserControlVisible = false;

        }
        #endregion
    }
}
