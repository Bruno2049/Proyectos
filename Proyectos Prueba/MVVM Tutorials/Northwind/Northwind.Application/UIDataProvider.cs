using System;
using System.Collections.Generic;
using System.Linq;
using Northwind.Data;

namespace Northwind.Application
{
    public class UIDataProvider : IUIDataProvider
    {

        private NorthwindEntities _northwindEntities =  new NorthwindEntities();
        public IList<Customer> GetCustomers()
        {
            return _northwindEntities.Customers.ToList();
        }

        public Customer GetCustomer(string customerID)
        {
            return _northwindEntities.Customers.Single(c => c.CustomerID == customerID);
        }
    }
}
