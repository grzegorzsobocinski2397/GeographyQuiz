using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class SummaryViewModel : BaseViewModel
    {
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
            MessengerInstance.Register<NotificationMessage<List<CountryAnswer>>>(this, PrepareSummary);

            // Initialize collections
            SummaryList = new List<SummaryString>();
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Prepares the last page of the game
        /// </summary>
        /// <param name="listOfCountries">List of answers from the quiz</param>
        private void PrepareSummary(NotificationMessage<List<CountryAnswer>> message)
        {
            // Checks if the notification message matches 
            if (message.Notification == "Summary")
            {
                // Creates summary string from every answer 
                foreach (var countryAnswer in message.Content)
                {
                    if(countryAnswer.GameMode == GameMode.Capitals)
                    {
                        if (countryAnswer.WasUserRight == true)
                        {
                            // Formats the string 
                            string summaryText = string.Format("For {0} you answered {1}, which was the correct answer.", countryAnswer.Name, countryAnswer.Capital);
                            var summaryString = new SummaryString(summaryText, "Green");
                            SummaryList.Add(summaryString);
                        }
                        else if (countryAnswer.WasUserRight == false)
                        {
                            string correctAnswer = string.Empty;

                            // Searches for the correct answer in the countries list 
                            foreach (var country in CountriesList)
                            {
                                if (countryAnswer.Name == country.Name)
                                    correctAnswer = country.Capital;
                            }

                            // Formats the string 
                            string summaryText = string.Format("For {0} you answered {1}. Correct answer is {2}.", countryAnswer.Name, countryAnswer.Capital, correctAnswer);
                            var summaryString = new SummaryString(summaryText, "Red");
                            SummaryList.Add(summaryString);
                        }
                    }
                    else if (countryAnswer.GameMode == GameMode.Countries)
                    {
                        if (countryAnswer.WasUserRight == true)
                        {
                            // Formats the string 
                            string summaryText = string.Format("For {0} you answered {1}, which was the correct answer.", countryAnswer.Name, countryAnswer.Capital);
                            var summaryString = new SummaryString(summaryText, "Green");
                            SummaryList.Add(summaryString);
                        }
                        else if (countryAnswer.WasUserRight == false)
                        {
                            string correctAnswer = string.Empty;

                            // Searches for the correct answer in the countries list 
                            foreach (var country in CountriesList)
                            {
                                if (countryAnswer.Name == country.Name)
                                    correctAnswer = country.Capital;
                            }

                            // Formats the string 
                            string summaryText = string.Format("For {0} you answered {1}. Correct answer is {2}.", countryAnswer.Name, countryAnswer.Capital, correctAnswer);
                            var summaryString = new SummaryString(summaryText, "Red");
                            SummaryList.Add(summaryString);
                        }
                    }
                    
                }
            }
        }
        #endregion
        
    }
}
