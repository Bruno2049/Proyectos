using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Waiter.Classes
{
    static class MessagesListClass
    {
        internal static string userNameNotValid = "The User Name is not valid.";

        internal static string passwordNotValid = "The Password is not valid.";

        internal static string noConnection = "Unable to connect to the server.";

        internal static string UserNotAllowed = "You are not allowed to login to this application.";

        internal static string UserNotValid= "Your User Name or Password is not valid.";

        internal static string ConnectionTestSucceed = "Test Succeed!";

        internal static string ConnectionTestFailed = "Unable to connect to the {0}";

        internal static string FieldIsEmpty = "{0} is mandatory.";

        internal static string ActionSucceed = "{0} Succeed!";

        internal static string ActionFailed = "{0} Failed!";

        internal static string UnhandleException = "Error";

        internal static string NoRowIsSelected = "No Row Is Selected!";

        internal static string SelectedTableIsOccupied = "Table : {0} is occupied by another order. Are you sure you want to select this table?";

        internal static string OrderIsLock="Order No : {0} is locked by another user. Please try again later.";

        internal static string OrderIsNotAvailable= "Order No : {0} is not available anymore.";

        internal static string ProductIsNotAvailable = "{0} is not available anymore.";

        internal static string ProductIsUneditable = "This row can not be {0} because it is uneditable.";

        internal static string OrderIsEmpty = "Order is Empty!";

        internal static string OrderCreatedSuccessfully = "This order successfully saved with the OrderNo: {0}";

        internal static string OrderEditedSuccessfully = "OrderNo: {0} Edited successfully.";

        internal static string NoTableIsSelected = "No table is selected!";
    }
}
