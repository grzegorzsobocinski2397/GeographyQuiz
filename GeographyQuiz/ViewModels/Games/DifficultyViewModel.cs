using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class DifficultyViewModel : BaseViewModel
    {

        #region Private Members
        /// <summary>
        /// Contains information about the game and game difficulty that user chose.
        /// </summary>
        private string[] GameSettings { get; set; } 
        #endregion
        #region Commands
        /// <summary>
        /// Difficulty level clicked by the user.
        /// </summary>
        public ICommand DifficultyCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DifficultyViewModel()
        {
            // Initialize the array
            GameSettings = new string[2];

            // Creates commands
            DifficultyCommand = new RelayParameterCommand((parameter) => StartGame(parameter));

            // Registers the messages
            MessengerInstance.Register<NotificationMessage<string[]>>(this, GameChosen);
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Catches the notification from the <see cref="ChooseGameViewModel"/>.
        /// </summary>
        /// <param name="message">Array that contains the information about game mode.</param>
        private void GameChosen(NotificationMessage<string[]> message)
        {
            // Checks if the notificaion matches
            if(message.Notification == "GameChosen")
            {
                GameSettings = message.Content;
            }
        }
        /// <summary>
        /// Sends the information to the <see cref="GameListViewModel"/> about game mode and difficulty.
        /// </summary>
        /// <param name="parameter">Game difficulty.</param>
        private void StartGame(object parameter)
        {
            // Adds the difficulty level to the array
            GameSettings[1] = parameter as string;
            
            ChangePage(ApplicationPage.GamePage);

            // Sends the difficulty level inside MVVM Light notification message to the CapitalsViewModel
            MessengerInstance.Send(new NotificationMessage<string[]>(GameSettings, "DifficultyChosen"));
            
        }
        #endregion
    }
}
