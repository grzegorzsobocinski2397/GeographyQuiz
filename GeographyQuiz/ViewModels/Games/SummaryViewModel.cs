using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class SummaryViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// Countries used in current game.
        /// </summary>
        private static List<Country> gameCountries = new List<Country>();
        /// <summary>
        /// Answer for the current question.
        /// </summary>
        private string correctAnswer;
        /// <summary>
        /// What game mode user played in the current game.
        /// </summary>
        private GameMode gameMode;
        #endregion
        #region Public Properties
        /// <summary>
        /// List of countries to display on the <see cref="SummaryPage"/>.
        /// </summary>
        public List<SummaryString> SummaryList { get; set; }
        #endregion
        #region Commands
        /// <summary>
        /// Returns back to the main menu
        /// </summary>
        public ICommand ReturnCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SummaryViewModel()
        {
            // Creates commands
            ReturnCommand = new RelayCommand(() => ChangePage(ApplicationPage.ChooseGamePage));
            // Registers MVVM light communication
            MessengerInstance.Register<NotificationMessage<List<Tuple<bool, Country, Country>>>>(this, PrepareSummary);
            MessengerInstance.Register<NotificationMessage<GameMode>>(this, SetGameMode);
            // Initialize collections
            SummaryList = new List<SummaryString>();
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Get the game mode from the <see cref="GameViewModel"/>
        /// </summary>
        /// <param name="gameModeMessage"></param>
        private void SetGameMode(NotificationMessage<GameMode> gameModeMessage)
        {
            if (gameModeMessage.Notification == "GameModeSummary")
                gameMode = gameModeMessage.Content;
        }
        /// <summary>
        /// Prepares the last page of the game
        /// </summary>
        /// <param name="listOfCountries">List of answers from the quiz</param>
        private void PrepareSummary(NotificationMessage<List<Tuple<bool, Country, Country>>> message)
        {
            // Checks if the notification message matches 
            if (message.Notification == "Summary")
            {
                // Adds the answered countries so searching for correct answer is faster than 
                // looking for the answer in the database
                foreach (var answer in message.Content)
                {
                    gameCountries.Add(answer.Item2);
                }

                // Check answers and style text correctly
                if (gameMode == GameMode.Capitals)
                    CapitalsGameModeSummary(message.Content);
                else 
                    CountriesGameModeSummary(message.Content);
            }
        }

        private void CapitalsGameModeSummary(List<Tuple<bool, Country, Country>> answers)
        {
            
            // Creates summary string from every answer 
            foreach (var answer in answers)
            {
                if (answer.Item1 == true)
                {
                    // Formats the string 
                    string summaryText = string.Format("For {0} you answered {1}, which was the correct answer.",
                        answer.Item2.Name, answer.Item2.Capital);
                    var summaryString = new SummaryString(summaryText, "Green");
                    SummaryList.Add(summaryString);
                }
                else if (answer.Item1 == false)
                {
                    // Formats the string 
                    string summaryText = string.Format("For {0} you answered {1}. Correct answer is {2}.",
                        answer.Item2.Name, answer.Item3.Capital, answer.Item2.Capital);
                    var summaryString = new SummaryString(summaryText, "Red");
                    SummaryList.Add(summaryString);
                }
            }
        }
        private void CountriesGameModeSummary(List<Tuple<bool, Country, Country>> answers)
        {
            // Creates summary string from every answer 
            foreach (var answer in answers)
            {

                if (answer.Item1 == true)
                {
                    // Formats the string 
                    string summaryText = string.Format("For {0} you answered {1}, which was the correct answer.",
                        answer.Item2.Name, answer.Item2.Capital);

                    var summaryString = new SummaryString(summaryText, "Green");
                    SummaryList.Add(summaryString);
                }
                else if (answer.Item1 == false)
                {
                   
                    // Formats the string 
                    string summaryText = string.Format("For {0} you answered {1}. Correct answer is {2}.",
                        answer.Item2.Name, answer.Item3.Capital, answer.Item2.Capital);

                    var summaryString = new SummaryString(summaryText, "Red");
                    SummaryList.Add(summaryString);
                }
            }
        }
        #endregion
    }
}
