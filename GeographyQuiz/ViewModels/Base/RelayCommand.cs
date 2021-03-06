﻿using System;
using System.Windows.Input;

namespace GeographyQuiz
{
    /// <summary>
    /// Base class for the relay command without parameter
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Action to run
        /// </summary>
        private Action action;

       
        public event EventHandler CanExecuteChanged = (sender,e) => { };

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action)
        {
            this.action = action;
        }
        /// <summary>
        /// Command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
