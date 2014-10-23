using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Timers;
using System.Windows.Media.Animation;
using Restaurant.Classes;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for CashierOrdersListUserControl.xaml
    /// </summary>
    public partial class CashierOrdersListUserControl : UserControl
    {
        #region These are global object which will be use to communicate with the database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        #endregion

        DataView ordersListDataView;

        public CashierOrdersListUserControl()
        {
            InitializeComponent();
            ordersListDataView = new DataView();
            FilterTextBox.FilterTextBox.TextChanged += new TextChangedEventHandler(FilterTextBox_TextChanged);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        internal void LoadOrders()
        {
            this.Cursor = Cursors.Wait;
            if (objDatabaseClass.CheckConnection())
            {
                string spName = "Get_Orders_List_For_Cashier";
                try
                {
                    DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                    ordersListDataView = ds.Tables[0].DefaultView;
                    OrdersGridView.ItemsSource = ordersListDataView;
                    OrdersGridView.Items.GroupDescriptions.Clear();
                    OrdersGridView.Items.GroupDescriptions.Add(new PropertyGroupDescription("TableNo"));
                    OrdersGridView.SelectedIndex = -1;
                }
                catch (SqlException)
                { }
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int64 temp;
            if (Int64.TryParse(FilterTextBox.Text, out temp))
            {
                ordersListDataView.RowFilter = "TableNo Like '" + FilterTextBox.Text + "%' OR OrderNo= " + temp + "";
            }
            else
            {
                ordersListDataView.RowFilter = "TableNo Like '" + FilterTextBox.Text + "%'";
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            FilterTextBox.Text = "";
            LoadOrders();
            this.Cursor = Cursors.Arrow;
        }

        private void btnShowDetails_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            Window mainWindow = FindRootParent((FrameworkElement)this.Parent);
            ((Storyboard)mainWindow.Resources["StoryboardFadeOut"]).Begin(mainWindow);
            Button btn = (Button)sender;
            DataRowView selectedRow = (DataRowView)btn.Tag;
            OrdersGridView.SelectedItem = selectedRow;
            Int64 orderNo = (Int64)selectedRow["OrderNo"];
            string tableNo = selectedRow["TableNo"].ToString();
            Byte state = (Byte)selectedRow["State"];
            OrderDetailInCashierWindow objOrderDetailInCashierWindow = new OrderDetailInCashierWindow(orderNo, tableNo,state);
            objOrderDetailInCashierWindow.Owner = mainWindow;
            bool? dg = objOrderDetailInCashierWindow.ShowDialog();
            this.Cursor = Cursors.Wait;
            if (dg == true)
            {
                FilterTextBox.Text = "";
                LoadOrders();
                try
                {
                    TablesStatusUserControl ts = (TablesStatusUserControl)mainWindow.FindName("TablesStatus");
                    ts.GetAllTables();
                }
                catch (Exception)
                {
                    //Unable to refresh the tables list.
                }
            }
            ((Storyboard)mainWindow.Resources["StoryboardFadeIn"]).Begin(mainWindow);
            this.Cursor = Cursors.Arrow;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            Window mainWindow = FindRootParent((FrameworkElement)this.Parent);
            ((Storyboard)mainWindow.Resources["StoryboardFadeOut"]).Begin(mainWindow);
            Button btn = (Button)sender;
            DataRowView selectedRow = (DataRowView)btn.Tag;
            OrdersGridView.SelectedItem = selectedRow;
            Int64 orderNo = (Int64)selectedRow["OrderNo"];
            string tableNo = selectedRow["TableNo"].ToString();
            Byte state = (Byte)selectedRow["State"];
            NewOrderWindow objNewOrderWindow = new NewOrderWindow(orderNo,tableNo);
            objNewOrderWindow.Owner = mainWindow;
            bool? dg = objNewOrderWindow.ShowDialog();
            if (dg == true)
            {
                FilterTextBox.Text = "";
                LoadOrders();
                try
                {
                    TablesStatusUserControl ts = (TablesStatusUserControl)mainWindow.FindName("TablesStatus");
                    ts.GetAllTables();
                }
                catch (Exception)
                {
                    //Unable to refresh the tables list.
                }
            }
            ((Storyboard)mainWindow.Resources["StoryboardFadeIn"]).Begin(mainWindow);
            this.Cursor = Cursors.Arrow;
        }

        private Window FindRootParent(FrameworkElement element)
        {
            while (element.Parent.GetType().BaseType != typeof(Window))
            {
                element = (FrameworkElement)element.Parent;
            }
            return (Window)element.Parent;
        }
    }
}
