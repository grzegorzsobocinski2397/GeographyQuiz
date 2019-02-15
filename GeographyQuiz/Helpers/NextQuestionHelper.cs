

using System;
using System.Collections.Generic;
using System.Linq;

namespace GeographyQuiz
{
    public class NextQuestionHelper
    {
        #region Private Members
        /// <summary>
        /// Shuffles the arrays.
        /// </summary>
        private Shuffler shuffler = new Shuffler();
        /// <summary>
        /// Current questions to be placed in the buttons.
        /// </summary>
        private List<Country> currentQuestions = new List<Country>();
        #endregion
        #region Public Properties
        public string QuestionString { get; set; }
        #endregion
        #region Public Methods
        /// <summary>
        /// Returns buttons with random countries and one button with correct answer.
        /// </summary>
        /// <param name="countries">List of countries that are currently avaiable in the game</param>
        /// <param name="gameMode">Mode of the game</param>
        /// <param name="numberOfQuestionsLeft">How many questions user has yet to answer</param>
        /// <returns></returns>
        public Tuple<List<Button>, Country> GetQuestion(List<Button> buttons, List<Country> countries, GameMode gameMode, int numberOfQuestionsLeft)
        {
            // Random numbers 
            int[] ChosenNumbers = shuffler.Shuffle(numberOfQuestionsLeft + 10);

            // Adds 4 countries to the current question 
            for (int i = 0; i < 4; i++)
            {
                currentQuestions.Add(countries.ElementAt(ChosenNumbers[i]));
            }

            int[] Questions = shuffler.Shuffle(4);

            // Correct answer is also random
            var CorrectAnswer = currentQuestions.ElementAt(Questions[shuffler.RandomNumber.Next(0, 3)]);
            
            // Change the buttons content based on the gamemode
            if (gameMode == GameMode.Capitals)
            {
                // Changes the button content and selects the true answer
                for (int j = 0; j < 4; j++)
                {
                    // Resets the values for buttons
                    ButtonReset(buttons[j]);

                    // Changes the button content
                    buttons[j].Content = currentQuestions.ElementAt(Questions[j]).Name;

                    // Selects button with correct answer
                    if (buttons[j].Content == CorrectAnswer.Name)
                        buttons[j].IsCorrect = true;
                }

                QuestionString = string.Format("{0} to stolica, którego kraju?", CorrectAnswer.Capital);
            }
            else if (gameMode == GameMode.Countries)
            {
                // Changes the button content and selects the true answer
                for (int j = 0; j < 4; j++)
                {
                    // Resets the values for buttons
                    ButtonReset(buttons[j]);

                    // Changes the button content
                    buttons[j].Content = currentQuestions.ElementAt(Questions[j]).Capital;

                    // Selects button with correct answer
                    if (buttons[j].Content == CorrectAnswer.Capital)
                        buttons[j].IsCorrect = true;
                }
                QuestionString = string.Format("Stolicą państwa {0} jest?", CorrectAnswer.Name);
            }

            // Clears current questions list
            currentQuestions.Clear();
            return new Tuple<List<Button>, Country>(buttons, CorrectAnswer);
        }

        #endregion
        #region Private Methods
        /// <summary>
        /// Resets the button values to default.
        /// </summary>
        /// <param name="button">Button to reset.</param>
        /// <returns></returns>
        private Button ButtonReset(Button button)
        {
            button.IsCorrect = false;
            button.IsSelected = false;
            button.BackgroundColor = "Blue";

            return button;
        }
        #endregion
    }
}

