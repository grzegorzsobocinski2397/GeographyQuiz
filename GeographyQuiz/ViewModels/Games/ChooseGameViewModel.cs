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
            object[] gameChosen = new object[2];

            // Adds the chosen gamemode to the array
            if (parameter as string == "Capitals")
                gameChosen[0] = GameMode.Capitals;
            else if (parameter as string == "Countries")
                gameChosen[0] = GameMode.Countries;

            // Changes the current page
            ChangePage(ApplicationPage.DifficultyPage);

            // Sends the message to the GameViewModel
            MessengerInstance.Send(new NotificationMessage<object[]>(gameChosen, "GameChosen"));

        }
        #endregion
    }
}
