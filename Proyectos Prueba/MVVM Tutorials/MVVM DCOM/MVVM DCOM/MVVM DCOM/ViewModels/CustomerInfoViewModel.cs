namespace MVVM_DCOM.ViewModels
{
    using System;
    using System.ComponentModel;

    internal class CustomerInfoViewModel : INotifyPropertyChanged
    {
        private string _info;

        public String Info
        {
            get
            {
                return _info;
            }

            set 
            {
                _info = value;
                OnPropertyChanged("info");
            }
        }

        #region Interface INotifyPropertyChanged
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
