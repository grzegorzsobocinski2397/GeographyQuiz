

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
        #endregion
        #region Public Properties
        public string QuestionString { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NextQuestionHelper()
        {

        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Returns buttons with random countries and one button with correct answer.
        /// </summary>
        /// <param name="countries">List of countries that are currently avaiable in the game</param>
        /// <param name="gameMode">Mode of the game</param>
        /// <param name="numberOfQuestionsLeft">How many questions user has yet to answer</param>
        /// <returns></returns>
        public List<Button> NextQuestion(List<Country> countries, string gameMode, int numberOfQuestionsLeft)
        {
            // Creates new list of buttons
            List<Button> ButtonList = new List<Button>()
            {
                new Button{ },
                new Button{ },
                new Button{ },
                new Button{ }
            };
            
            List<Country> CurrentQuestions = new List<Country>();

            // Random numbers 
            int[] ChosenNumbers = shuffler.Shuffle(numberOfQuestionsLeft + 10);

            // Adds 4 countries to the current question 
            for (int i = 0; i < 4; i++)
            {
                CurrentQuestions.Add(countries.ElementAt(ChosenNumbers[i]));
            }

            int[] Questions = shuffler.Shuffle(4);

            // Correct answer is also random
            var CorrectAnswer = CurrentQuestions.ElementAt(Questions[shuffler.RandomNumber.Next(0, 3)]);
            
            

            if (gameMode == "Capitals")
            {
                // Changes the button content and selects the true answer
                for (int j = 0; j < 4; j++)
                {
                    // Resets the values for buttons
                    ButtonReset(ButtonList[j]);

                    // Changes the button content
                    ButtonList[j].Content = CurrentQuestions.ElementAt(Questions[j]).Name;

                    // Selects button with correct answer
                    if (ButtonList[j].Content == CorrectAnswer.Name)
                        ButtonList[j].IsCorrect = true;
                }

                QuestionString = string.Format("{0} to stolica, którego kraju?", CorrectAnswer.Capital);
            }
            else if (gameMode == "Countries")
            {

                // Changes the button content and selects the true answer
                for (int j = 0; j < 4; j++)
                {
                    // Resets the values for buttons
                    ButtonReset(ButtonList[j]);

                    // Changes the button content
                    ButtonList[j].Content = CurrentQuestions.ElementAt(Questions[j]).Capital;

                    // Selects button with correct answer
                    if (ButtonList[j].Content == CorrectAnswer.Capital)
                        ButtonList[j].IsCorrect = true;
                }
                QuestionString = string.Format("Stolicą państwa {0} jest?", CorrectAnswer.Name);
            }
            return ButtonList;
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

