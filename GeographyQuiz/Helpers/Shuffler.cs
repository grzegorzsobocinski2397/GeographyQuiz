using System;

namespace GeographyQuiz
{
    /// <summary>
    /// Shuffles the arrays
    /// </summary>
    public class Shuffler
    {
        #region Public Properties
        /// <summary>
        /// Random number that is used throughout this class
        /// </summary>
        public Random RandomNumber { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public Shuffler()
        {
            // Initialize random number
            RandomNumber = new Random();
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Randomize the elements
        /// </summary>
        /// <param name="numberOfElements">Size of the array</param>
        /// <returns></returns>
        public int[] Shuffle(int numberOfElements)
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
