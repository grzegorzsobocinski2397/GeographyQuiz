using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyQuiz
{
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

        public Country()
        {

        }
    }
}
