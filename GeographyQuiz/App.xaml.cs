using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace GeographyQuiz
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Connection string to the local database
        /// </summary>
        string connectionString = GeographyQuiz.Properties.Settings.Default.GeographyQuizDBConnectionString;

        /// <summary>
        /// Contains all the countries from the local database 
        /// </summary>
        public ObservableCollection<Country> CountriesList { get; set; }


        public App()
        {
            // Creates new Observable Collection of countries
            CountriesList = new ObservableCollection<Country>();

            // Create new datatable 
            DataTable CountriesTable = new DataTable();

            // Connects to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Opens the SQL Conenction
                conn.Open();

                // SQL statement for getting all the countries from the database
                SqlDataAdapter myCommand = new SqlDataAdapter("SELECT * FROM Countries", conn);
                
                // Fill the datatable with data from the database
                myCommand.Fill(CountriesTable);
                
                // Create new Country object from the acquired table
                foreach (DataRow row in CountriesTable.Rows)
                {
                    // Creates new Country
                    Country country = new Country()
                    {
                        Id = int.Parse(row.ItemArray.GetValue(0).ToString()),
                        Capital = row.ItemArray.GetValue(1).ToString(),
                        Name = row.ItemArray.GetValue(2).ToString(),
                        DifficultyLevel = int.Parse(row.ItemArray.GetValue(3).ToString()),
                        Region = row.ItemArray.GetValue(4).ToString(),

                    };

                    // Adds the country to the ObservableCollection
                    CountriesList.Add(country);
                }
            }
            

        }

    }
}
