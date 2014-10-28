using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Northwind.Application;
using Northwind.Data;
using System.Collections.ObjectModel;

namespace Northwind.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly IUIDataProvider _dataProvider;

        public ObservableCollection<ToolViewModel> Tools { get; set; }

        public string SelectedCustomerID { get; set; }

        private IList<Customer> _customers;
        public IList<Customer> Customers
        {
            get
            {
                if (_customers == null)
                {
                    GetCustomers();
                }
                return _customers;
            }
        }

        public string Name
        {
            get { return "Northwind"; }
        }

        public string ControlPanelName
        {
            get { return "Control Panel"; }
        }

        public MainWindowViewModel(IUIDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            Tools = new ObservableCollection<ToolViewModel>();
            Tools.Add(new CustomerDetailViewModel(_dataProvider, "ALFKI"));
        }

        public void ShowCustomerDetails()
        {
            if (string.IsNullOrWhiteSpace(SelectedCustomerID))
                throw new InvalidOperationException("SelectedCustomerID can't be null");
            CustomerDetailViewModel customerDetailViewModel = GetCustomerDetailsViewModel(SelectedCustomerID);
            if (customerDetailViewModel == null)
            {
                customerDetailViewModel = new CustomerDetailViewModel(_dataProvider, SelectedCustomerID);
                Tools.Add(customerDetailViewModel);
            }

            SetCurrentTool(customerDetailViewModel);
        }

        private void SetCurrentTool(CustomerDetailViewModel currentTool)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(Tools);
            if (collectionView != null)
            {
                if (collectionView.MoveCurrentTo(currentTool) != true)
                {
                    throw new InvalidOperationException(
                        "Could not find the current tool");
                }
            }
        }

        private CustomerDetailViewModel GetCustomerDetailsViewModel(string customerID)
        {
            return Tools
                .OfType<CustomerDetailViewModel>()
                .FirstOrDefault(c => c.Customer.CustomerID == customerID);

        }

        private void GetCustomers()
        {
            _customers = _dataProvider.GetCustomers();
        }
    }

}
