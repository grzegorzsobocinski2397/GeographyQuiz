using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class GameViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// Boolean is true if the user answered correctly,
        /// Country is the question,
        /// String is the user answer.
        /// </summary>
        private List<Tuple<bool, Country, Country>> answers = new List<Tuple<bool, Country, Country>>();
        /// <summary>
        /// List of countries from the database.
        /// </summary>
        private static List<Country> databaseCountries = GetCountries();
        /// <summary>
        /// Next question helper class.
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
        public Country CorrectAnswer { get; set; }
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
        public List<Country> ListofCountries { get; set; }
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
            // Initialize countries list
            ListofCountries = new List<Country>();

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
            // Initialize 
            string correctAnswerString = string.Empty;
            Country userAnswer = new Country();

            // Check the answer based on the game mode 
            if (gameMode == GameMode.Capitals)
            {
                userAnswer = ListofCountries.Where(c => c.Name == answer.Content).FirstOrDefault();
                correctAnswerString = CorrectAnswer.Name;
            }
            else
            {
                userAnswer = ListofCountries.Where(c => c.Capital == answer.Content).FirstOrDefault();
                correctAnswerString = CorrectAnswer.Capital;
            }

            // Color the buttons according to answers 
            foreach (var button in ListOfButtons)
            {
                IsBreakOn = true;

                // Users choice 
                if (button.Content == answer.Content)
                    button.IsSelected = true;


                // Sets the color of buttons
                if (button.IsSelected == true && button.Content == correctAnswerString)
                    button.BackgroundColor = "Green";
                else if (button.IsSelected == true && button.Content != correctAnswerString)
                    button.BackgroundColor = "Red";
                else if (button.Content == correctAnswerString)
                    button.BackgroundColor = "Green";
                else
                    button.BackgroundColor = "Blue";
            }

            // If the user's answer matcher the correct answer then he gets a point
            if (answer.Content == correctAnswerString)
            {
                // Decrease number of questions by 1
                NumberOfQuestionsLeft--;
                // Increase user score by one
                NumberOfCorrectAnswers++;
                // Adds the used country into summary, true because user was right
                answers.Add(new Tuple<bool, Country, Country>(true, CorrectAnswer, userAnswer));
            }
            else
            {
                // Decrease number of questions by 1
                NumberOfQuestionsLeft--;
                // Adds the used country into summary, false because user was wrong 
                answers.Add(new Tuple<bool, Country, Country>(false, CorrectAnswer, userAnswer));
            }
        }
        /// <summary>
        /// Starts the game based on the gamemode and difficulty mode.
        /// </summary>
        /// <param name="message">Notification Message sent from the <see cref="DifficultyViewModel"/></param>
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
                ListofCountries = countriesFilter.GetCountries(NumberOfQuestionsLeft, databaseCountries);

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

                // Gets the buttons with content from helper class and set the correct answer
                Tuple<List<Button>, Country> questions = nextQuestion.GetQuestion(ListOfButtons, ListofCountries, gameMode, NumberOfQuestionsLeft);
                ListOfButtons = questions.Item1;
                CorrectAnswer = questions.Item2;

                // Removes the country from the list of questions
                if (gameMode == GameMode.Capitals)
                    ListofCountries.Remove(CorrectAnswer);
                else
                    ListofCountries.Remove(CorrectAnswer);

                // Sets the question string based on the gamemode
                Question = nextQuestion.QuestionString;
            }
            else
            {
                // Changes the page to summary
                ChangePage(ApplicationPage.SummaryPage);
                // Sends the message to the SummaryViewModel
                MessengerInstance.Send(new NotificationMessage<GameMode>(gameMode, "GameModeSummary"));
                MessengerInstance.Send(new NotificationMessage<List<Tuple<bool, Country, Country>>>(answers, "Summary"));
            }
        }
        #endregion
    }
}
