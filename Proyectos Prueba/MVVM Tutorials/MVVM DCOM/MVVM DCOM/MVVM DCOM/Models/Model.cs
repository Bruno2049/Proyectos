

namespace MVVM_DCOM.Models
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public class Model : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _customerName;

        public string CustomerName
        {
            get
            {
                return _customerName;
            }

            set
            {
                _customerName = value;
                OnPropertyChanged("CustomerName");
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

        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        public bool IsValid
        {
            get
            {
                //foreach (string property in ValidateProperties)
                //{
                //    if (GetValidationError(property) != null)
                //        return false;
                //}

                //return true;

                return ValidateProperties.All(property => GetValidationError(property) == null);
                
            }
        }

        #endregion

        #region Validation

        static readonly string[] ValidateProperties =
        {
            "CustomerName"
        };

        private string GetValidationError(String propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "CustomerName":
                    error = ValidateCustomerName();
                    break;
            }
            return error;
        }

        public string ValidateCustomerName()
        {
            if (String.IsNullOrWhiteSpace(CustomerName))
            {
                return "Customer name cannot empty";
            }

            return null;
        }

        #endregion

    }
}
