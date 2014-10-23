namespace Enterprice_MVVM.Windows
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.ObjectModel;


    public abstract class ViewModel : ObservableObject , IDataErrorInfo
    {

        public string Error
        {
            get
            {
                throw new NotSupportedException();
            }
            
        }

        public string this[string columnName]
        {
            get
            {
                return OnValidate(columnName);
            }
        }

        protected virtual string OnValidate(string propertyName)
        {
            var context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            var results = new Collection<ValidationResult>();
            var isvalid = Validator.TryValidateObject(this, context, results, true);

            return !isvalid ? results[0].ErrorMessage : null;
        }
    }
}