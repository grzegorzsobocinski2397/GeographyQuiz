using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class DifficultyViewModel : BaseViewModel
    {

        #region Public Properties
        /// <summary>
        /// Difficulty level chosen by the user
        /// </summary>
        public int DifficultyLevel { get; set; }
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
        public DifficultyViewModel()
        {   
            // Creates commands
            DifficultyCommand = new RelayParameterCommand((parameter) => StartGame(parameter));
        }
        #endregion
        #region Private Methods
        private void StartGame(object parameter)
        {
            // Changes the difficulty level
            DifficultyLevel = int.Parse(parameter as string);

            // Sends the difficulty level inside MVVM Light notification message to the CapitalsViewModel
            MessengerInstance.Send(new NotificationMessage<int>(DifficultyLevel, "DifficultyChosen"));
        }
        #endregion
    }
}
