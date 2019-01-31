namespace GeographyQuiz
{
    /// <summary>
    /// Basic class for the countries
    /// </summary>
    public class Country
    {
        #region Private Members
        private int id;
        private string name;
        private string capital;
        private string region;
        private int difficultyLevel;
        #endregion
        #region Public Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        

        public string Capital
        {
            get { return capital; }
            set { capital = value; }
        }
        

        public string Region
        {
            get { return region; }
            set { region = value; }
        }
        

        public int DifficultyLevel
        {
            get { return difficultyLevel; }
            set { difficultyLevel = value; }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor for Country class.
        /// </summary>
        public Country()
        {

        }
        #endregion
    }
}
