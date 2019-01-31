using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class ChooseGameViewModel : BaseViewModel
    {
        #region Commands
        /// <summary>
        /// Starts new game based on the user's choice.
        /// </summary>
        public ICommand GameChosenCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ChooseGameViewModel()
        {
            // Creates commands
            GameChosenCommand = new RelayParameterCommand((parameter) => GameChosen(parameter));
           
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Starts the selected game.
        /// </summary>
        /// <param name="parameter">Selected game by the user</param>
        private void GameChosen(object parameter)
        {
            // Creates array of two elements (game chosen, difficulty chosen)
            string[] gameChosen = new string[2];

            // Casts the parameter into string
            gameChosen[0] = parameter as string;

            // Changes the current page
            ChangePage(ApplicationPage.DifficultyPage);

            // Sends the message to the GameViewModel
            MessengerInstance.Send(new NotificationMessage<string[]>(gameChosen, "GameChosen"));

        }
        #endregion
    }
}
