using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class CapitalsListViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// Shuffles the arrays.
        /// </summary>
        private Shuffler Shuffler = new Shuffler();
        /// <summary>
        /// Get the countries based on the difficulty level.
        /// </summary>
        private GetCountriesHelper GetCountriesHelper = new GetCountriesHelper();
        /// <summary>
        /// Contains all the answers 
        /// </summary>
        private List<Country> summaryList = new List<Country>();
        #endregion
        #region Public Properties
        /// <summary>
        /// Number of questions left, it is equivalent to the difficulty level.
        /// </summary>
        public int NumberOfQuestionsLeft { get; set; }
        /// <summary>
        /// Informs the user of his current score and how many questions he has yet to answer
        /// </summary>
        public string ScoreInformation { get; set; }
        /// <summary>
        /// Number of questions answered correctly.
        /// </summary>
        public int NumberOfCorrectAnswers { get; set; }
        /// <summary>
        /// Correct answer for the current question.
        /// </summary>
        public Country CorrectAnswer { get; set; }
        public List<Button> ListOfButtons { get; set; }
        public Button FirstButton { get; set; }
        public Button SecondButton { get; set; }
        public Button ThirdButton { get; set; }
        public Button FourthButton { get; set; }
        public bool IsBreakOn { get; set; }
        /// <summary>
        /// Contains countries chosen by random on the difficulty level specified before
        /// </summary>
        public List<Country> CountriesForTheGame { get; set; }
        /// <summary>
        /// Question for the user
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// 4 random countries from the <see cref="CountriesForTheGame"/>
        /// </summary>
        public ObservableCollection<Country> CurrentQuestions { get; set; }
        #endregion
        #region Commands
        /// <summary>
        /// User's choice in the game.
        /// </summary>
        public ICommand AnswerCommand { get; set; }
        /// <summary>
        /// User clicked the window and wants to proceed.
        /// </summary>
        public ICommand BreakOverCommand { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default contructor
        /// </summary>
        public CapitalsListViewModel()
        {
            // Creates Lists and Observable Collections
            CountriesForTheGame = new List<Country>();
            CurrentQuestions = new ObservableCollection<Country>();

            // Creates commands 
            AnswerCommand = new RelayParameterCommand((parameter) => CheckTheAnswer(parameter));
            BreakOverCommand = new RelayCommand(() => NextQuestion());
            
            // Creates new buttons 
            FirstButton = new Button();
            SecondButton = new Button();
            ThirdButton = new Button();
            FourthButton = new Button();

            // Insert these buttons into a list so it's easier to reset them
            ListOfButtons = new List<Button>()
            {
                FirstButton,
                SecondButton,
                ThirdButton,
                FourthButton
            };

            // Registers the message 
            MessengerInstance.Register<NotificationMessage<int>>(this, StartGame);
        }
        #endregion
        #region Private Methods
        private void CheckTheAnswer(object parameter)
        {
            // Casts the parameter as a string
            Button answer = (Button)parameter;

            // Color the buttons according to answers 
            foreach(var button in ListOfButtons)
            {
                IsBreakOn = true;

                // Users choice 
                if (button.Content == answer.Content)
                    button.IsSelected = true;

                // Sets the color of buttons
                if (button.IsSelected == true && button.Content == CorrectAnswer.Name)
                    button.BackgroundColor = "Green";
                else if (button.IsSelected == true && button.Content != CorrectAnswer.Name)
                    button.BackgroundColor = "Red";
                else if(button.Content == CorrectAnswer.Name)
                    button.BackgroundColor = "Green";
                else
                    button.BackgroundColor = "Blue";
            }

            // If the user's answer matcher the correct answer then he gets a point
            if (answer.Content == CorrectAnswer.Name)
            {
                // Decrease number of questions by 1
                NumberOfQuestionsLeft--;
                // Increase user score by one
                NumberOfCorrectAnswers++;
                // Clear current questions collection
                CurrentQuestions.Clear();
                // Adds the used country into summary, true because user was rigth
                summaryList.Add(AddToSummary(answer.Content, true));
            }
            else
            {
                // Decrease number of questions by 1
                NumberOfQuestionsLeft--;
                // Clear current questions collection
                CurrentQuestions.Clear();
                // Adds the used country into summary, false because user was wrong 
                summaryList.Add(AddToSummary(answer.Content, false));

            }
        }
        /// <summary>
        /// Adds the country to the summary
        /// </summary>
        /// <param name="countryName">Country name to be added</param>
        /// <returns></returns>
        private Country AddToSummary(string countryName, bool wasUserRight)
        {
            // Looks up for the country in the database
            Country country = CountriesList.Where(c => c.Name == countryName).FirstOrDefault();
            // Changes the value based on the parameter
            country.WasUserRight = wasUserRight;
            return country;
        }
        private void StartGame(NotificationMessage<int> message)
        {
            // Continue if the message notification matches
            if (message.Notification == "DifficultyChosen")
            {
                // Number of questions is equivalent to the difficulty level
                NumberOfQuestionsLeft = message.Content;

                // Gets the countries based on the difficulty level
                CountriesForTheGame = GetCountriesHelper.GetCountries(message.Content, CountriesList);

                // Begins with the first question
                NextQuestion();
            }
        }

        /// <summary>
        /// Creates a new question
        /// </summary>
        private void NextQuestion()
        {
            if(NumberOfQuestionsLeft > 0)
            {
                // Break is over and buttons are now usable
                IsBreakOn = false;

                // Informs the user of his current score 
                ScoreInformation = string.Format("{0} questions left, you answered {1} correctly", NumberOfQuestionsLeft, NumberOfCorrectAnswers);

                // Random numbers 
                int[] ChosenNumbers = Shuffler.Shuffle(NumberOfQuestionsLeft+10);

                // Adds 4 countries to the current question 
                for (int i = 0; i < 4; i++)
                {
                    CurrentQuestions.Add(CountriesForTheGame.ElementAt(ChosenNumbers[i]));
                }

                int[] Questions = Shuffler.Shuffle(4);

                // Correct answer is also random
                CorrectAnswer = CurrentQuestions.ElementAt(Questions[Shuffler.RandomNumber.Next(0, 3)]);

                // Removes the answer from the current questions so it won't appear again
                CountriesForTheGame.Remove(CorrectAnswer);

                // Formats the question string 
                Question = string.Format("{0} jest stolicą, którego kraju?", CorrectAnswer.Capital);

                // Changes the button content and selects the true answer
                for (int j = 0; j < 4; j++)
                {
                    // Resets the values for buttons
                    ListOfButtons[j].IsCorrect = false;
                    ListOfButtons[j].IsSelected = false;
                    ListOfButtons[j].BackgroundColor = "Blue";

                    // Changes the button content
                    ListOfButtons[j].Content = CurrentQuestions.ElementAt(Questions[j]).Name;

                    // Selects button with correct answer
                    if (ListOfButtons[j].Content == CorrectAnswer.Name)
                        ListOfButtons[j].IsCorrect = true;
                }
            }
            else
            {
                // Changes the page to summary
                ChangePage(ApplicationPage.SummaryPage);
                // Sends the message to the SummaryViewModel
                MessengerInstance.Send(new NotificationMessage<List<Country>>(summaryList, "Summary"));
                
            }
            
        }
        #endregion
    }
}
