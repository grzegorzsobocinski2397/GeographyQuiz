using System.Collections.Generic;
using System.Linq;

namespace GeographyQuiz
   
{
    /// <summary>
    /// Returns specified amount of countries
    /// </summary>
    public class CountriesFilter
    {
        #region Private Members
        /// <summary>
        /// Shuffles the array.
        /// </summary>
        private Shuffler shuffler = new Shuffler();
        /// <summary>
        /// Countries to return.
        /// </summary>
        private List<Country> countriesList = new List<Country>();
        #endregion
        #region Public Methods
        /// <summary>
        /// Returns randomized countries based on the difficulty level.
        /// </summary>
        /// <param name="numberOfElements">How many countries should be returned</param>
        /// <param name="countriesList">Countries to be returned</param>
        /// <returns></returns>
        public List<Country> GetCountries(int numberOfElements, List<Country> databaseCountries)
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
            int[] ChosenNumbers = shuffler.Shuffle(numberOfElements+10);

            // Adds every country based on the difficulty level
            foreach (Country country in databaseCountries.ToList())
            {
                if (difficultyLevel >= country.DifficultyLevel)
                    databaseCountries.Remove(country);
            }

            // Adds specified amount of countries to the game
            for (int i = 0; i < numberOfElements+10; i++)
            {
                countriesList.Add(databaseCountries.ElementAt(ChosenNumbers[i]));
            }

            return countriesList;
        }
        #endregion
    }
}
