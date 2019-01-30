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
        /// Shuffles the arrays
        /// </summary>
        public Shuffler Shuffler { get; set; }
        #endregion
        #region Public Properties

        /// <summary>
        /// Informs the user of his current score and how many questions he has yet to answer
        /// </summary>
        public string ScoreInformation { get; set; }
        /// <summary>
        /// Number of questions answered correctly
        /// </summary>
        public int NumberOfCorrectAnswers { get; set; }
        /// <summary>
        /// Correct answer for the current question
        /// </summary>
        public Country CorrectAnswer { get; set; }
        /// <summary>
        /// First answer, but it doesn't need to be a first button
        /// </summary>
        public string FirstAnswer { get; set; }
        /// <summary>
        /// Second answer, but it doesn't need to be a first button
        /// </summary>
        public string SecondAnswer { get; set; }
        /// <summary>
        /// Third answer, but it doesn't need to be a first button
        /// </summary>
        public string ThirdAnswer { get; set; }
        /// <summary>
        /// Fourth answer, but it doesn't need to be a first button
        /// </summary>
        public string FourthAnswer { get; set; }
        /// <summary>
        /// Number of questions left, it is equivalent to the difficulty level 
        /// </summary>
        public int NumberOfQuestionsLeft { get; set; }

        public GetCountriesHelper GetCountriesHelper { get; set; }
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
        /// User's choice in the game
        /// </summary>
        public ICommand AnswerCommand { get; set; }
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

            Shuffler = new Shuffler();
            GetCountriesHelper = new GetCountriesHelper();

            // Registers the message 
            MessengerInstance.Register<NotificationMessage<int>>(this, StartGame);


        }
        #endregion
        #region Private Methods
        private void CheckTheAnswer(object parameter)
        {
            // Casts the parameter as a string
            string answer = (string)parameter;
            // If the user's answer matcher the correct answer then he gets a point
            if (answer == CorrectAnswer.Name)
            {
                // Decrease number of questions by 1
                NumberOfQuestionsLeft--;
                // Increase user score by one
                NumberOfCorrectAnswers++;
                // Clear current questions collection
                CurrentQuestions.Clear();
                // Start a new question
                NextQuestion();
            }
            else
            {
                // Decrease number of questions by 1
                NumberOfQuestionsLeft--;
                // Clear current questions collection
                CurrentQuestions.Clear();
                // Starts a new question
                NextQuestion();
            }
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
            // Informs the user of his current score 
            ScoreInformation = string.Format("{0} questions left, you answered {1} correctly", NumberOfQuestionsLeft, NumberOfCorrectAnswers);

            // Random numbers 
            int[] ChosenNumbers = Shuffler.Shuffle(NumberOfQuestionsLeft);
            
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

            // Answers are placed randomly 
            FirstAnswer = CurrentQuestions.ElementAt(Questions[0]).Name;
            SecondAnswer = CurrentQuestions.ElementAt(Questions[1]).Name;
            ThirdAnswer = CurrentQuestions.ElementAt(Questions[2]).Name;
            FourthAnswer = CurrentQuestions.ElementAt(Questions[3]).Name;


        }



        #endregion
    }
}
