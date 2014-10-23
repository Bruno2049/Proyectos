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
using System.Timers;
using Restaurant.UserControls;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OrderDetailInKitchenWindow : Window
    {
        #region These are global object which will be use to communicate with the database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        #endregion
        private Int64 orderNo;
        private string tableNo;
        private OrderState state = OrderState.New;

        public OrderDetailInKitchenWindow(Int64 orderNo, string tableNo, OrderState state)
        {
            this.InitializeComponent();
            // Insert code required on object creation below this point.
            this.orderNo = orderNo;
            this.tableNo = tableNo;
            this.state = state;
            if (state == OrderState.Canceled)
            {
                btnPrint.IsEnabled = false;
                btnReadyToServe.IsEnabled = false;
                OrderGridView.IsEnabled = false;
            }
            txbOrderNo.Text = orderNo.ToString();
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
                GetOrderDetail();
                if (!LockOrder(orderNo, GlobalClass._UserID))
                {
                    MessageBox.Show(string.Format(ErrorMessages.Default.OrderIsLock,orderNo), "Order is locked!", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.DialogResult = false;
                }
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
            }
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            UnlockOrder(orderNo, GlobalClass._UserID);
        }

        private bool LockOrder(Int64 orderNo, int userID)
        {
            try
            {
                object[] spParams = new object[2];
                spParams[0] = orderNo;
                spParams[1] = userID;
                string spName = "Lock_Order";
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

        private void GetOrderDetail()
        {
            try
            {
                object[] spParams = new object[1];
                spParams[0] = orderNo;
                string spName = "Get_Order_Detail";
                DataSet ds = objSqlDatabase.ExecuteDataSet(spName, spParams);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        OrderGridView.ItemsSource = ds.Tables[0].DefaultView;
                        OrderGridView.SelectedIndex = -1;
                    }
                }
            }
            catch (SqlException)
            {

            }
        }

        private void CheckBoxNotEditableState_Checked(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            CheckBox cItem = (CheckBox)sender;
            DataRowView selectedRow = (DataRowView)cItem.Tag;
            OrderGridView.SelectedItem = selectedRow;
            Int64 orderDetailID = (Int64)selectedRow["OrderDetailID"];
            if (UpdateNotEditable(orderDetailID))
            {
                cItem.IsChecked = true;
            }
            this.Cursor = Cursors.Arrow;
        }

        private void CheckBoxNotEditableState_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            CheckBox cItem = (CheckBox)sender;
            DataRowView selectedRow = (DataRowView)cItem.Tag;
            OrderGridView.SelectedItem = selectedRow;
            cItem.IsChecked = true;
            this.Cursor = Cursors.Arrow;
        }

        private bool UpdateNotEditable(Int64 orderDetailID)
        {
            bool result = false;
            string spName = "Update_NotEditable_State";
            object[] spParams = new object[1];
            spParams[0] = orderDetailID;
            try
            {
                int temp = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                if (temp > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (SqlException)
            {
                result = false;
            }
            return result;
        }

        private void btnReadyToServe_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (objDatabaseClass.CheckConnection())
            {
                btnReadyToServe.IsEnabled = false;
                object[] spParams = new object[1];
                spParams[0] = orderNo;
                string spName = "Set_Order_ReadyToServe";
                try
                {
                    int result=(int)objSqlDatabase.ExecuteNonQuery(spName, spParams);
                    if (result > 0)
                    {
                        MessageBox.Show(string.Format(ErrorMessages.Default.OrderIsReady,orderNo));
                        this.DialogResult = true;
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show(ErrorMessages.Default.UnhandledException,"Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                btnReadyToServe.IsEnabled = true;
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            AutoPrintPage printOrder = new AutoPrintPage();
            PrintDialog pdg = new PrintDialog();
            if (pdg.ShowDialog() == true)
            {
                Visual content = printOrder.GetContent(orderNo, tableNo, state,true);
                if (content != null)
                {
                    pdg.PrintVisual(content, "Print Order #" + orderNo.ToString());
                }
            }
        }
    }
    public enum OrderState
    {
        New,
        Edited,
        Canceled,
        ReadyToServe,
        Served,
        Finished
    }
}