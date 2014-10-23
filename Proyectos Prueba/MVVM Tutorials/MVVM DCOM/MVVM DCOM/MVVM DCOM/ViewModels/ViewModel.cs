namespace MVVM_DCOM.ViewModels
{
    using Models;
    using System.ComponentModel;

    public class ViewModel:INotifyPropertyChanged
    {
        public ViewModel()
        {
            Model = new Model
            {
                CustomerName = "David"
            };
        }

        public Model Model { get; set; }

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
    }
}
