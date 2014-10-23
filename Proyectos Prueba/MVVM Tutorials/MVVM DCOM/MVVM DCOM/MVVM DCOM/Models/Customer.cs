namespace MVVM_DCOM.Models
{
    using System;
    using System.ComponentModel;

    public class Customer : INotifyPropertyChanged, IDataErrorInfo
    {   
        public Customer(String customerName)
        {
            Name = customerName;
        }
        
        private string _name;

        public String Name
	    {
		    get 
            { 
                return _name;
            }
		    set 
            { 
                _name = value;
                OnPropertyChanged("Name");
            }
	    }

        #region interface INotifyPropertyChanged

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

        #region Inerface IDataErrorInfo

        public string Error
        {
            get;
            private set;
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    Error = String.IsNullOrWhiteSpace(Name) ? "Name cannot be null or empty" : null;
                }

                return Error;
            }
        }

        #endregion
    }
}
