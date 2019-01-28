using GalaSoft.MvvmLight;
using System.ComponentModel;

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
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public BaseViewModel()
        {

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