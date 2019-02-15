using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class GameViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// Next question helper class
        /// </summary>
        private NextQuestionHelper nextQuestion = new NextQuestionHelper();

        /// <summary>
        /// Current game mode.
        /// </summary>
        private GameMode gameMode = GameMode.None;
        /// <summary>
        /// Get the countries based on the difficulty level.
        /// </summary>
        private CountriesFilter countriesFilter = new CountriesFilter();
        /// <summary>
        /// Contains all the answers 
        /// </summary>
        private List<CountryAnswer> summaryList = new List<CountryAnswer>();
        #endregion
        #region Public Properties
        /// <summary>
        /// Number of questions left, it is equivalent to the difficulty level.
        /// </summary>
        public int NumberOfQuestionsLeft { get; set; }
        /// <summary>
        /// Number of questions answered correctly.
        /// </summary>
        public int NumberOfCorrectAnswers { get; set; }
        /// <summary>
        /// Correct answer for the current question.
        /// </summary>
        public string CorrectAnswer { get; set; }
        /// <summary>
        /// Buttons on the screen.
        /// </summary>
        public List<Button> ListOfButtons { get; set; }
        /// <summary>
        /// True if user answered a question.
        /// </summary>
        public bool IsBreakOn { get; set; }
        /// <summary>
        /// Contains countries chosen by random on the difficulty level specified before.
        /// </summary>
        public List<Country> CountriesForTheGame { get; set; }
        /// <summary>
        /// Question for the user.
        /// </summary>
        public string Question { get; set; }
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
        /// Default contructor.
        /// </summary>
        public GameViewModel()
        {
            // Creates Lists and Observable Collections
            CountriesForTheGame = new List<Country>();

            // Creates commands 
            AnswerCommand = new RelayParameterCommand((parameter) => CheckTheAnswer(parameter));
            BreakOverCommand = new RelayCommand(() => NextQuestion());

            
            // Creates list of buttons 
            ListOfButtons = new List<Button>()
            {
                new Button(),
                new Button(),
                new Button(),
                new Button(),
            };

            // Registers the message 
            MessengerInstance.Register<NotificationMessage<object[]>>(this, StartGame);
        }
        #endregion
        #region Private Methods
        private void CheckTheAnswer(object parameter)
        {
            // Casts the parameter as a string
            Button answer = (Button)parameter;

            // Color the buttons according to answers 
            foreach (var button in ListOfButtons)
            {
                IsBreakOn = true;

                // Users choice 
                if (button.Content == answer.Content)
                    button.IsSelected = true;

                // Sets the color of buttons
                if (button.IsSelected == true && button.Content == CorrectAnswer)
                    button.BackgroundColor = "Green";
                else if (button.IsSelected == true && button.Content != CorrectAnswer)
                    button.BackgroundColor = "Red";
                else if (button.Content == CorrectAnswer)
                    button.BackgroundColor = "Green";
                else
                    button.BackgroundColor = "Blue";
            }

            // If the user's answer matcher the correct answer then he gets a point
            if (answer.Content == CorrectAnswer)
            {
                // Decrease number of questions by 1
                NumberOfQuestionsLeft--;
                // Increase user score by one
                NumberOfCorrectAnswers++;
                // Adds the used country into summary, true because user was right
                summaryList.Add(AddToSummary(answer.Content, true));
            }
            else
            {
                // Decrease number of questions by 1
                NumberOfQuestionsLeft--;
                // Adds the used country into summary, false because user was wrong 
                summaryList.Add(AddToSummary(answer.Content, false));
            }
        }
        /// <summary>
        /// Adds the country to the summary
        /// </summary>
        /// <param name="countryName">Country name to be added</param>
        /// <returns></returns>
        private CountryAnswer AddToSummary(string answer, bool wasUserRight)
        {
            // Looks up for the country in the database
            Country country = new Country();

            // Creates new country answer and adds that to the summary list  
            if (gameMode == GameMode.Capitals)
            {
                country = CountriesList.Where(c => c.Name == CorrectAnswer).FirstOrDefault();
                CountryAnswer countryAnswer = new CountryAnswer()
                {
                    Capital = country.Capital,
                    Name = answer,
                    WasUserRight = wasUserRight,
                    GameMode = GameMode.Capitals

                };
                return countryAnswer;

            }
            else
            {
                country = CountriesList.Where(c => c.Capital == CorrectAnswer).FirstOrDefault();
                CountryAnswer countryAnswer = new CountryAnswer()
                {
                    Capital = answer,
                    Name = country.Name,
                    WasUserRight = wasUserRight,
                    GameMode = GameMode.Countries
                };

                return countryAnswer;
            }


        }
        private void StartGame(NotificationMessage<object[]> message)
        {
            // Continue if the message notification matches
            if (message.Notification == "DifficultyChosen")
            {
                // Number of questions is equivalent to the difficulty level
                NumberOfQuestionsLeft = int.Parse((string)message.Content[1]);

                // Sets the current game mode
                gameMode = (GameMode)message.Content[0];

                // Gets the countries based on the difficulty level and game mode
                CountriesForTheGame = countriesFilter.GetCountries(NumberOfQuestionsLeft, CountriesList);

                // Begins with the first question
                NextQuestion();
            }
        }

        /// <summary>
        /// Creates a new question
        /// </summary>
        private void NextQuestion()
        {
            if (NumberOfQuestionsLeft > 0)
            {
                // Break is over and buttons are now usable
                IsBreakOn = false;

                // Gets the buttons with content from helper class
                var buttonsList = nextQuestion.NextQuestion(CountriesForTheGame, gameMode, NumberOfQuestionsLeft);

                // Assign content of buttons
                for (int i = 0; i < 4; i++)
                {
                    ListOfButtons[i].BackgroundColor = buttonsList[i].BackgroundColor;
                    ListOfButtons[i].Content = buttonsList[i].Content;
                    ListOfButtons[i].IsCorrect = buttonsList[i].IsCorrect;
                    ListOfButtons[i].IsSelected = buttonsList[i].IsSelected;

                    if (ListOfButtons[i].IsCorrect == true)
                        CorrectAnswer = ListOfButtons[i].Content;
                }

                // Sets the correct answer based on the gamemode
                if (gameMode == GameMode.Capitals)
                    CountriesForTheGame.Remove(CountriesForTheGame.Where(c => c.Name == CorrectAnswer).FirstOrDefault());
                else if (gameMode == GameMode.Countries)
                    CountriesForTheGame.Remove(CountriesForTheGame.Where(c => c.Capital == CorrectAnswer).FirstOrDefault());

                // Sets the question string based on the gamemode
                Question = nextQuestion.QuestionString;
            }
            else
            {
                // Changes the page to summary
                ChangePage(ApplicationPage.SummaryPage);
                // Sends the message to the SummaryViewModel
                MessengerInstance.Send(new NotificationMessage<List<CountryAnswer>>(summaryList, "Summary"));
            }
        }
        #endregion
    }
}
