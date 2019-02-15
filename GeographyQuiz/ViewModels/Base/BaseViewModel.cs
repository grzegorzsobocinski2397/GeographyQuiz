using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace GeographyQuiz
{
    /// <summary>
    /// Base View Model for all the view models that use NotifyPropertyChanged and MVVM Light communication.
    /// </summary>
    public class BaseViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Private Members
        /// <summary>
        /// Connection string to the local database.
        /// </summary>
        private static readonly string connectionString = GeographyQuiz.Properties.Settings.Default.GeographyQuizDBConnectionString;
        #endregion
        #region Public Methods
        /// <summary>
        /// Gets all the countries from the database.
        /// </summary>
        /// <returns></returns>
        public static List<Country> GetCountries()
        {
            // Creates new countries list 
            List<Country> CountriesList = new List<Country>();

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

            return CountriesList;
        }
        /// <summary>
        /// Changes the current page of the main window 
        /// </summary>
        /// <param name="page"></param>
        public void ChangePage(ApplicationPage page)
        {
            ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = page;
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