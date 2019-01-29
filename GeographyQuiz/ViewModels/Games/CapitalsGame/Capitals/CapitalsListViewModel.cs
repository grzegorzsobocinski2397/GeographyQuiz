using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyQuiz
{
    public class CapitalsListViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Default contructor
        /// </summary>
        public CapitalsListViewModel()
        {
            MessengerInstance.Register<NotificationMessage<int>>(this, StartGame);

        }
        #endregion
        #region Public Properties
        #endregion
        #region Private Methods
        private void StartGame(NotificationMessage<int> message)
        {
            if(message.Notification == "DifficultyChosen")
            {
               
                
            }
        }
        #endregion
    }
}
