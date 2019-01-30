namespace GeographyQuiz
{
    /// <summary>
    /// Basic class for the countries
    /// </summary>
    public class Country
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string capital;

        public string Capital
        {
            get { return capital; }
            set { capital = value; }
        }
        private string region;

        public string Region
        {
            get { return region; }
            set { region = value; }
        }
        private int difficultyLevel;

        public int DifficultyLevel
        {
            get { return difficultyLevel; }
            set { difficultyLevel = value; }
        }
        /// <summary>
        /// True if the user answered right
        /// </summary>
        private bool wasUserRight;

        public bool WasUserRight
        {
            get { return wasUserRight; }
            set { wasUserRight = value; }
        }

        public Country()
        {

        }
    }
}
