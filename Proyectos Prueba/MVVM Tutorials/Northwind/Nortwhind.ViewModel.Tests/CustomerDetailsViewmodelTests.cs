using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Application;
using Northwind.Data;
using Northwind.ViewModel;
using Rhino.Mocks;

namespace Nortwhind.ViewModel.Tests
{
    [TestClass]
    public class CustomerDetailsViewmodelTests
    {
        [TestMethod]
        public void Ctor_Always_CallsGetCustomer()
        {
            //Arrange
            IUIDataProvider uiDataProviderMock = MockRepository.GenerateMock<IUIDataProvider>();
            const string expectedID = "EXPECTEDID";
            uiDataProviderMock.Expect(c => c.GetCustomer(expectedID)).Return(new Customer());

            //Act
            CustomerDetailViewModel target = new CustomerDetailViewModel(uiDataProviderMock, expectedID);

            //Assert
            uiDataProviderMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void Customer_Always_ReturnsCustomerFromGetCustomer()
        {
            //Arrange
            IUIDataProvider uiDataProviderStub = MockRepository.GenerateStub<IUIDataProvider>();
            const string expectedID = "EXPECTEDID";

            Customer expectedCustomer = new Customer
            {
                CustomerID = expectedID
            };

            uiDataProviderStub.Stub(c => c.GetCustomer(expectedID)).Return(expectedCustomer);

            //Act
            CustomerDetailViewModel target = new CustomerDetailViewModel(uiDataProviderStub, expectedID);

            //Assert
            Assert.AreEqual(expectedCustomer, target.Customer);
        }

        [TestMethod]
        public void Customer_Always_ReturnsCompanyName()
        {
            //Arrange
            IUIDataProvider uiDataProviderStub = MockRepository.GenerateStub<IUIDataProvider>();

            const string expectedID = "EXPECTEDID";
            const string expectedCompanyName = "EXPECTEDNAME";

            Customer expectedCustomer = new Customer
            {
                CustomerID = expectedID,
                CompanyName = expectedCompanyName
            };

            uiDataProviderStub.Stub(c => c.GetCustomer(expectedID)).Return(expectedCustomer);

            //Act
            CustomerDetailViewModel target = new CustomerDetailViewModel(uiDataProviderStub, expectedID);

            //Assert
            Assert.AreEqual(expectedCompanyName, target.DisplayName);
        }
    }
}
