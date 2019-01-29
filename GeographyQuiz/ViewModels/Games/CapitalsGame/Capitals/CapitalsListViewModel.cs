using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeographyQuiz
{
    public class CapitalsListViewModel : BaseViewModel
    {
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
        /// <summary>
        /// Random number that is used throughout this class
        /// </summary>
        public Random RandomNumber { get; set; }
        /// <summary>
        /// Contains countries based on the difficulty level
        /// </summary>
        public List<Country> CountriesBasedOnDifficulty { get; set; }
        /// <summary>
        /// Contains countries chosen by random on the difficulty level specified before
        /// </summary>
        public ObservableCollection<Country> CountriesForTheGame { get; set; }
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
            CountriesBasedOnDifficulty = new List<Country>();
            CountriesForTheGame = new ObservableCollection<Country>();
            CurrentQuestions = new ObservableCollection<Country>();

            // Creates commands 
            AnswerCommand = new RelayParameterCommand((parameter) => CheckTheAnswer(parameter));
            // Creates new random
            RandomNumber = new Random();

            // Registers the message 
            MessengerInstance.Register<NotificationMessage<int>>(this, PrepareQuestions);


        }
        #endregion
        #region Private Methods
        private void CheckTheAnswer(object parameter)
        {
            // Casts the parameter as a string
            string answer = (string)parameter;
            // If the user's answer matcher the correct answer then he gets a point
            if(answer == CorrectAnswer.Name)
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
        private void PrepareQuestions(NotificationMessage<int> message)
        {
            // Continue if the message notification matches
            if (message.Notification == "DifficultyChosen")
            {
                switch (message.Content)
                {
                    case 30:
                        GetCountries(30);
                        break;
                    case 60:
                        GetCountries(60);
                        break;
                    case 90:
                        GetCountries(90);
                        break;
                }
                // Begins with the first question
                NextQuestion();
            }

        }

        /// <summary>
        /// Creates list of countries for the game
        /// </summary>
        /// <param name="numberOfElements">Number of countries for the game</param>
        private void GetCountries(int numberOfElements)
        {
            NumberOfQuestionsLeft = numberOfElements;
            // Sets the difficulty level based on the number of questions
            int difficultyLevel;

            if (numberOfElements == 90)
                difficultyLevel = 3;
            else if (numberOfElements == 60)
                difficultyLevel = 2;
            else
                difficultyLevel = 1;

            int[] ChosenNumbers = Shuffle(numberOfElements);

            // Adds every country based on the difficulty level
            foreach (Country country in CountriesList)
            {
                if (country.DifficultyLevel <= difficultyLevel)
                    CountriesBasedOnDifficulty.Add(country);
            }

            // Adds specified amount of countries to the game
            for (int i = 0; i < numberOfElements; i++)
            {
                CountriesForTheGame.Add(CountriesBasedOnDifficulty.ElementAt(ChosenNumbers[i]));
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
            int[] ChosenNumbers = Shuffle(NumberOfQuestionsLeft);

            // Loop that goes till there are no questions left 

            // Adds 4 countries to the current question 
            for (int i = 0; i < 4; i++)
            {
                CurrentQuestions.Add(CountriesForTheGame.ElementAt(ChosenNumbers[i]));

            }

            int[] Questions = Shuffle(4);
            // Correct answer is also random
            CorrectAnswer = CurrentQuestions.ElementAt(Questions[RandomNumber.Next(0,3)]);

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
      
        /// <summary>
        /// Randomize the elements
        /// </summary>
        /// <param name="numberOfElements">Size of the array</param>
        /// <returns></returns>
        private int[] Shuffle(int numberOfElements)
        {

            // Creates new array
            int[] ChosenNumbers = new int[numberOfElements];

            // Populates the array list like (1,2,3,4...,30)
            for (int i = 0; i < numberOfElements; i++)
            {
                ChosenNumbers[i] = i;
            }

            // Length of the array, created for a better readability of the for loop 
            int n = ChosenNumbers.Length;

            // Shuffler based on Fisher Yates Shuffle
            for (int i = 0; i < n; i++)
            {
                // For example; when i = 3, then rand.Next(10-3) and r may be 0-7
                // In this case it will be 7
                int r = i + RandomNumber.Next(n - i);
                // t equals ChosenNumbers[7]
                int t = ChosenNumbers[r];
                // ChosenNumbers[10] equals ChosenNumbers[3]
                ChosenNumbers[r] = ChosenNumbers[i];
                // Then ChosenNumbers[3] equals 7
                ChosenNumbers[i] = t;
            }

            return ChosenNumbers;
        }

        #endregion
    }
}
