using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace GeographyQuiz
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class BaseViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Connection string to the local database
        /// </summary>
        private readonly string connectionString = GeographyQuiz.Properties.Settings.Default.GeographyQuizDBConnectionString;

        /// <summary>
        /// Contains all the countries from the local database 
        /// </summary>
        public List<Country> CountriesList { get; set; }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public BaseViewModel()
        {
            // Creates new Observable Collection of countries
            CountriesList = new List<Country>();

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
                        Name = row.ItemArray.GetValue(1).ToString(),
                        Capital = row.ItemArray.GetValue(2).ToString(),
                        DifficultyLevel = int.Parse(row.ItemArray.GetValue(3).ToString()),
                        Region = row.ItemArray.GetValue(4).ToString(),

                    };

                    // Adds the country to the ObservableCollection
                    CountriesList.Add(country);
                }
            }
        }

        /// <summary>
        /// Changes the current page ofo the main window 
        /// </summary>
        /// <param name="page"></param>
        public void ChangePage(ApplicationPage page)
        {
            ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ChangePage(page);
        }
        #endregion
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}