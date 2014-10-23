using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Globalization;
using System.Data.Common;
using System.Linq;
using Microsoft.Reporting.WinForms;
using Restaurant.Classes;
using System.Drawing.Printing;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OrderDetailInCashierWindow : Window
    {
        #region These are global object which will be use to communicate with the database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        #endregion
        private Int64 currentOrderNo;
        private decimal currentTotalPrice = 0;
        private byte currentOrderState = 0;
        private string tableNo;
        bool? dialogResult = null;

        public OrderDetailInCashierWindow(Int64 orderNo, string tableNo, byte orderState)
        {
            this.InitializeComponent();
            // Insert code required on object creation below this point.
            this.currentOrderNo = orderNo;
            this.tableNo = tableNo;
            this.currentOrderState = orderState;
            txbTableNo.Text = tableNo;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //Here we should call the method which load initial data to the controls
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (objDatabaseClass.CheckConnection())
            {
                if (CheckOrderState(currentOrderNo, currentOrderState))
                {
                    if (!PopulateOrdersComboBox())
                    {
                        this.DialogResult = dialogResult;
                    }
                    else
                    {
                        cmbOrderNo.SelectedIndex = -1;
                        List<TableOrder> list = (List<TableOrder>)cmbOrderNo.ItemsSource;
                        var query = from l in list
                                    where l.OrderNo == currentOrderNo
                                    select l;
                        try
                        {
                            cmbOrderNo.SelectedItem = query.First();
                        }
                        catch
                        {
                            dialogResult = true;
                            this.DialogResult = dialogResult;
                        }
                    }
                }
                else
                {
                    dialogResult = true;
                    MessageBox.Show(string.Format(ErrorMessages.Default.OrderStateChanged,currentOrderNo.ToString()), "", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.DialogResult = dialogResult;
                }
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = dialogResult;
            }
            this.Cursor = Cursors.Arrow;
        }

        private bool CheckOrderState(Int64 orderNo, byte orderState)
        {
            string spName = "Get_Order_State";
            object[] spParams = new object[1];
            spParams[0] = orderNo;
            try
            {
                byte newOrderState = (byte)objSqlDatabase.ExecuteScalar(spName, spParams);
                if (newOrderState == orderState)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        // This method return the list of orders which are active for an specified tableNo
        private List<TableOrder> GetOrders(string tableNo)
        {
            List<TableOrder> li = new List<TableOrder>();
            string spName = "Get_Orders_Of_A_Table";
            object[] spParams = new object[1];
            spParams[0] = tableNo;
            DataSet ds = objSqlDatabase.ExecuteDataSet(spName, spParams);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        li.Add(new TableOrder(Convert.ToInt64(ds.Tables[0].Rows[i]["OrderNo"]), Convert.ToByte(ds.Tables[0].Rows[i]["State"])));
                    }
                }
            }
            return li;
        }

        private bool PopulateOrdersComboBox()
        {
            bool result = false;
            cmbOrderNoIsActive = false;
            cmbOrderNo.ItemsSource = null;
            List<TableOrder> ordersList = GetOrders(tableNo);
            cmbOrderNo.ItemsSource = ordersList;
            if (ordersList.Count > 0)
            {
                if (ordersList.Count > 1)
                {
                    txbOrderNoHeader.Text = "Order No ( " + ordersList.Count.ToString() + " )";
                    txbOrderNoHeader.ToolTip = "There are " + ordersList.Count.ToString() + " active orders.";
                }
                else
                {
                    txbOrderNoHeader.Text = "Table No";
                    txbOrderNoHeader.ToolTip = null;
                }

                result = true;
            }
            else
            {
                result = false;
            }
            cmbOrderNoIsActive = true;
            return result;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            UnlockOrder(currentOrderNo, GlobalClass._UserID);
            this.DialogResult = dialogResult;
        }

        private string LockOrder(Int64 orderNo, int userID)
        {
            try
            {
                object[] spParams = new object[2];
                spParams[0] = orderNo;
                spParams[1] = userID;
                string spName = "Lock_Order_New";
                string result = (string)objSqlDatabase.ExecuteScalar(spName, spParams);
                return result;
            }
            catch (SqlException)
            {
                return "Exception";
            }
        }

        private bool UnlockOrder(Int64 orderNo, int userID)
        {
            try
            {
                object[] spParams = new object[2];
                spParams[0] = orderNo;
                spParams[1] = userID;
                string spName = "Unlock_Order";
                int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private DataTable GetOrderDetail(Int64 orderNo)
        {
            try
            {
                object[] spParams = new object[1];
                spParams[0] = orderNo;
                string spName = "Get_Order_Detail_For_Cashier";
                DataSet ds = objSqlDatabase.ExecuteDataSet(spName, spParams);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        return ds.Tables[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        private void PopUpSelectOrder_MouseLeave(object sender, MouseEventArgs e)
        {
            PopUpSelectOrder.IsOpen = false;
        }

        private void borderOrderNo_MouseEnter(object sender, MouseEventArgs e)
        {
            PopUpSelectOrder.IsOpen = true;
        }

        bool cmbOrderNoIsActive = false;
        private void cmbOrderNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (cmbOrderNoIsActive)
            {
                if (cmbOrderNo.SelectedIndex > -1)
                {
                    UnlockOrder(currentOrderNo, GlobalClass._UserID);
                    currentOrderNo = ((TableOrder)cmbOrderNo.SelectedItem).OrderNo;
                    currentOrderState = ((TableOrder)cmbOrderNo.SelectedItem).State;
                    DataTable dt = GetOrderDetail(currentOrderNo);
                    if (dt != null)
                    {
                        OrderGridView.ItemsSource = dt.DefaultView;
                        OrderGridView.SelectedIndex = -1;
                        var query = from o in dt.AsEnumerable()
                                    select o.Field<decimal>("RowPrice");
                        currentTotalPrice = 0;
                        foreach (decimal temp in query)
                        {
                            currentTotalPrice += temp;
                        }
                        string totalPriceStr = currentTotalPrice.ToString();
                        if (totalPriceStr.EndsWith(".00"))
                        {
                            totalPriceStr = totalPriceStr.Split('.')[0];
                        }
                        txbTotalPrice.Text = totalPriceStr + " $";
                        expanderCheckOut.IsExpanded = true;
                    }
                    string locker = LockOrder(currentOrderNo, GlobalClass._UserID);
                    if (string.IsNullOrEmpty(locker) == false & locker != "Exception")
                    {
                        txbLocker.Text = locker;
                        expanderCheckOut.IsEnabled = false;
                        expanderCancelOrder.IsEnabled = false;
                        btnPrint.IsEnabled = false;
                        gridLocker.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        expanderCheckOut.IsEnabled = true;
                        expanderCancelOrder.IsEnabled = true;
                        btnPrint.IsEnabled = true;
                        txbLocker.Text = "";
                        gridLocker.Visibility = Visibility.Hidden;
                    }
                }
            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (currentTotalPrice != 0 & OrderGridView.Items.Count > 0)
            {
                if (currentOrderState != 4)
                {
                    MessageBoxResult msgResult = MessageBox.Show(string.Format(ErrorMessages.Default.OrderNotServedYet, currentOrderNo.ToString()), "", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                    if (msgResult == MessageBoxResult.No)
                    {
                        this.Cursor = Cursors.Arrow;
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txbCustomerName.Text))
                {
                    MessageBoxResult msgRes = MessageBox.Show(ErrorMessages.Default.CustomerNameNotEntered, "", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                    if (msgRes == MessageBoxResult.No)
                    {
                        txbCustomerName.SelectAll();
                        txbCustomerName.Focus();
                        this.Cursor = Cursors.Arrow;
                        return;
                    }
                }
                string spName = "Check_Out_Order";
                object[] spParams = new object[3];
                spParams[0] = currentOrderNo;
                spParams[1] = currentTotalPrice;
                if (string.IsNullOrEmpty(txbCustomerName.Text))
                {
                    spParams[2] = DBNull.Value;
                }
                else
                {
                    spParams[2] = txbCustomerName.Text;
                }
                if (objDatabaseClass.CheckConnection())
                {
                    try
                    {
                        int result = -1;
                        result = (int)objSqlDatabase.ExecuteScalar(spName, spParams);
                        if (result > 0)
                        {
                            dialogResult = true;
                            MessageBox.Show(string.Format(ErrorMessages.Default.OrderCheckOutSucceed, currentOrderNo.ToString()), "", MessageBoxButton.OK);
                            if (!PopulateOrdersComboBox())
                            {
                                UnlockOrder(currentOrderNo, GlobalClass._UserID);
                                this.DialogResult = dialogResult;
                            }
                            else
                            {
                                cmbOrderNo.SelectedIndex = -1;
                                cmbOrderNo.SelectedIndex = 0;
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show(string.Format(ErrorMessages.Default.OrderCheckOutFailed, currentOrderNo.ToString()), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (SqlException)
                    {
                    }
                }
                else
                {
                    MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(string.Format(ErrorMessages.Default.OrderNoIsNotCompleteToCheckOut, currentOrderNo.ToString()), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (!string.IsNullOrEmpty(txbCancelReason.Text))
            {
                string spName = "Cancel_Order";
                object[] spParams = new object[2];
                spParams[0] = currentOrderNo;
                spParams[1] = txbCancelReason.Text;
                if (objDatabaseClass.CheckConnection())
                {
                    try
                    {
                        int result = -1;
                        result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                        if (result > 0)
                        {
                            dialogResult = true;
                            MessageBox.Show(string.Format(ErrorMessages.Default.OrderCancelSucceed, currentOrderNo.ToString()), "", MessageBoxButton.OK);
                            if (!PopulateOrdersComboBox())
                            {
                                UnlockOrder(currentOrderNo, GlobalClass._UserID);
                                this.DialogResult = dialogResult;
                            }
                            else
                            {
                                cmbOrderNo.SelectedIndex = -1;
                                cmbOrderNo.SelectedIndex = 0;
                            }
                            txbCancelReason.Text = "";
                        }
                        else
                        {
                            MessageBox.Show(string.Format(ErrorMessages.Default.OrderCancelFailed, currentOrderNo.ToString()), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (SqlException)
                    {
                    }
                }
                else
                {
                    MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(string.Format(ErrorMessages.Default.FieldEmpty,"Cancel Cause"),"Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            if (OrderGridView.ItemsSource != null)
            {
                DataTable dtOrderDetail=((DataView)OrderGridView.ItemsSource).Table;

                DataTable dtRestaurantInfo = GlobalClass.GetRestaurantInfo();
                string AddressLine1 = "";
                string AddressLine2 = "";
                if (dtRestaurantInfo != null)
                {
                    if (dtRestaurantInfo.Rows.Count > 0)
                    {
                        AddressLine1 = dtRestaurantInfo.Rows[0]["RestaurantName"].ToString() + ", " + dtRestaurantInfo.Rows[0]["Address"].ToString();
                        AddressLine2 = "Phone: " + dtRestaurantInfo.Rows[0]["PhoneNumber"].ToString() + "    " + dtRestaurantInfo.Rows[0]["WebsiteURL"].ToString() + "    " + dtRestaurantInfo.Rows[0]["Email"].ToString();
                    }
                }
                ReportParameter[] arrParams = new ReportParameter[6];
                arrParams[0] = new ReportParameter("rpm_Date", GlobalClass.GetCurrentDateTime().ToShortDateString());
                arrParams[1] = new ReportParameter("rpm_CustomerName",txbCustomerName.Text);
                arrParams[2] = new ReportParameter("rpm_OrderNo", txbOrderNo.Text);
                arrParams[3] = new ReportParameter("rpm_TableNo", txbTableNo.Text);
                arrParams[4] = new ReportParameter("rpm_AddressLine1", AddressLine1);
                arrParams[5] = new ReportParameter("rpm_AddressLine2", AddressLine2);
                string reportPath = Environment.CurrentDirectory + "\\ReportSrc";
                ReportPrintClass objReportPrintClass = new ReportPrintClass("PublicDataSet_OrderDetail", dtOrderDetail, reportPath + "\\Report_Receipt.rdlc", arrParams, 8.27, 11.69, 0.5, 0.5, 0.2, 0.2);

                PrinterSettings ps = new PrinterSettings();
                PrinterSettings.StringCollection printersList = PrinterSettings.InstalledPrinters;
                string[] arrPrinters = new string[printersList.Count];
                for (int i = 0; i < printersList.Count; i++)
                {
                    arrPrinters[i] = printersList[i];

                }
                Cursor = Cursors.Arrow;
                CustomPrintDialog objCustomPrintDialog = new CustomPrintDialog(arrPrinters);
                if (objCustomPrintDialog.ShowDialog() == true)
                {
                    objReportPrintClass.Print(objCustomPrintDialog.printerName, 1);
                }
                objReportPrintClass.Dispose();
            }
        }
    }

    public class TableOrder
    {
        public TableOrder(Int64 orderNo, byte state)
        {
            this.orderNo = orderNo;
            this.state = state;
        }

        private Int64 orderNo = 0;
        public Int64 OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }

        private byte state = 0;
        public byte State
        {
            get { return state; }
            set { state = value; }
        }
    }
}