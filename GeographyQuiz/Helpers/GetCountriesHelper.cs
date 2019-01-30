using System.Collections.Generic;
using System.Linq;

namespace GeographyQuiz
   
{
    /// <summary>
    /// Returns specified amount of countries
    /// </summary>
    public class GetCountriesHelper
    {
        #region Private Members
        /// <summary>
        /// Shuffles the array 
        /// </summary>
        private Shuffler Shuffler { get; set; }
        /// <summary>
        /// Countries based on the difficulty level
        /// </summary>
        private List<Country> CountriesBasedOnDifficulty { get; set; }
        /// <summary>
        /// Countries to return 
        /// </summary>
        private List<Country> CountriesForTheGame { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GetCountriesHelper()
        {
            // Creates new lists
            CountriesBasedOnDifficulty = new List<Country>();
            CountriesForTheGame = new List<Country>();

            // Initialize new shuffler 
            Shuffler = new Shuffler();
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Returns randomized countries based on the difficulty level
        /// </summary>
        /// <param name="numberOfElements">How many countries should be returned</param>
        /// <param name="countriesList">Countries to be returned</param>
        /// <returns></returns>
        public List<Country> GetCountries(int numberOfElements, List<Country> countriesList)
        {
            
            // Sets the difficulty level based on the number of questions
            int difficultyLevel;

            if (numberOfElements == 90)
                difficultyLevel = 3;
            else if (numberOfElements == 60)
                difficultyLevel = 2;
            else
                difficultyLevel = 1;

            // Shuffles the array 
            int[] ChosenNumbers = Shuffler.Shuffle(numberOfElements+10);

            // Adds every country based on the difficulty level
            foreach (Country country in countriesList)
            {
                if (country.DifficultyLevel <= difficultyLevel)
                    CountriesBasedOnDifficulty.Add(country);
            }

            // Adds specified amount of countries to the game
            for (int i = 0; i < numberOfElements+10; i++)
            {
                CountriesForTheGame.Add(CountriesBasedOnDifficulty.ElementAt(ChosenNumbers[i]));
            }

            return CountriesForTheGame;
        }
        #endregion
    }
}
