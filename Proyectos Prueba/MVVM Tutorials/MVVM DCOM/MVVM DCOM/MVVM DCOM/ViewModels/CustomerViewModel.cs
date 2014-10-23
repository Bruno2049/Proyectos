namespace MVVM_DCOM.ViewModels
{
    using Models;
    using Commands;
    using Views;
    using System.Windows.Input;

    internal class CustomerViewModel
    {
        private readonly Customer _customer;

        private readonly CustomerInfoViewModel _childViewModel;

        public CustomerViewModel()
        {
            _customer = new Customer("David");
            UpdateCommand = new CustomerUpdateCommand(this);
            _childViewModel = new CustomerInfoViewModel();
        }

        public Customer Customer
        {
            get { return _customer; }
        }

        public CustomerInfoViewModel CustomerInfo
        {
            get
            {
                return _childViewModel;
            }
        }

        public ICommand UpdateCommand
        {
            get;
            private set;
        }

        public void SaveChanges()
        {
            //Debug.Assert(false, string.Format("{0} was update.", Customer.Name));
            var view = new CustomerInfoView {DataContext = _childViewModel};

            _childViewModel.Info = Customer.Name + " was updated in the data base.";

            view.ShowDialog();
        }

    }
}
