
namespace GeographyQuiz
{
    public class CountryAnswer : Country
    {
        #region Private Members
        /// <summary>
        /// True if user selected correct answer
        /// </summary>
        private bool wasUserRight;
        /// <summary>
        /// Game mode on which game was played
        /// </summary>
        private string gameMode;
        #endregion
        #region Public Properties
        /// <summary>
        /// True if user selected correct answer
        /// </summary>
        public bool WasUserRight
        {
            get { return wasUserRight; }
            set { wasUserRight = value; }
        }
        /// <summary>
        /// Game mode on which game was played
        /// </summary>
        public string GameMode
        {
            get { return gameMode; }
            set { gameMode = value; }
        }
     
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CountryAnswer()
        {
            GameMode = string.Empty;
            WasUserRight = false;
        }
        #endregion
    }
}
