using System;
using System.ComponentModel;
using System.Windows.Input;

namespace ViewModel.InterfaceBase
{
    public class CommandBase : ICommand, INotifyPropertyChanged
    {
        private readonly Predicate<Object> _canExecute;
        private readonly Action<Object> _executeAction;
        private bool _isEnabled;

        public CommandBase(Predicate<Object> canExecute, Action<object> executeAction)
        {
            _canExecute = canExecute;
            _executeAction = executeAction;
            _isEnabled = false;
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                //Si es diferente al valor anterior
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            if (_executeAction != null)
                _executeAction(parameter);
            UpdateCanExecuteState();
        }

        #endregion

        public void UpdateCanExecuteState()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        #region Implementation of INotifyPropertyChanged
        public  event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}