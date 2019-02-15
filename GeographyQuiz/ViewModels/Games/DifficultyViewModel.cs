using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class DifficultyViewModel : BaseViewModel
    {

        #region Private Members
        /// <summary>
        /// Current game settings
        /// </summary>
        private object[] gameSettings = new object[2];
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
            // Creates commands
            DifficultyCommand = new RelayParameterCommand((parameter) => StartGame(parameter));

            // Registers the messages
            MessengerInstance.Register<NotificationMessage<object[]>>(this, GameChosen);
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Catches the notification from the <see cref="ChooseGameViewModel"/>.
        /// </summary>
        /// <param name="message">Array that contains the information about game mode.</param>
        private void GameChosen(NotificationMessage<object[]> message)
        {
            // Checks if the notificaion matches
            if (message.Notification == "GameChosen")
                gameSettings = message.Content;
        }
        /// <summary>
        /// Sends the information to the <see cref="GameListViewModel"/> about game mode and difficulty.
        /// </summary>
        /// <param name="parameter">Game difficulty.</param>
        private void StartGame(object parameter)
        {
            // Casts the difficulty mode into string
            gameSettings[1] = parameter as string;

            ChangePage(ApplicationPage.GamePage);

            // Sends the difficulty level inside MVVM Light notification message to the CapitalsViewModel
            MessengerInstance.Send(new NotificationMessage<object[]>(gameSettings, "DifficultyChosen"));

        }
        #endregion
    }
}
