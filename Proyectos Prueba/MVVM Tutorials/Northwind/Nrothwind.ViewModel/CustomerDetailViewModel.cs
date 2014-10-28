using Northwind.Application;
using Northwind.Data;

namespace Northwind.ViewModel
{
    public class CustomerDetailViewModel : ToolViewModel
    {
        private readonly IUIDataProvider _dataProvider;
        public Customer Customer { get; set; }

        public CustomerDetailViewModel(IUIDataProvider dataProvider, string customerID)
        {
            _dataProvider = dataProvider;
            Customer = _dataProvider.GetCustomer(customerID);
            DisplayName = Customer.CompanyName;
        }
    }
}
