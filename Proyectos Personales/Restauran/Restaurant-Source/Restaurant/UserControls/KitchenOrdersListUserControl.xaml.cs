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
using System.Windows.Media.Animation;
using Restaurant.UserControls;


namespace Restaurant
{
    /// <summary>
    /// Interaction logic for KitchenOrdersListUserControl.xaml
    /// </summary>
    public partial class KitchenOrdersListUserControl : UserControl
    {
        #region These are global object which will be use to communicate with the database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        #endregion

        DataView ordersListDataView = new DataView();

        public KitchenOrdersListUserControl()
        {
            InitializeComponent();
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
                string spName = "Get_Orders_List_For_Kitchen";
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

        private void btnShowDetail_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            Window mainWindow=FindRootParent((FrameworkElement)this.Parent);
            ((Storyboard)mainWindow.Resources["StoryboardFadeOut"]).Begin(mainWindow);
            Button btn = (Button)sender;
            DataRowView selectedRow = (DataRowView)btn.Tag;
            OrdersGridView.SelectedItem = selectedRow;
            Int64 orderNo = (Int64)selectedRow["OrderNo"];
            string tableNo= selectedRow["TableNo"].ToString();
            Byte state = (Byte)selectedRow["State"];
            OrderState orderState= OrderState.New;
            if(state==0)
            {
                orderState= OrderState.New;
            }
            else if(state==1)
            {
                orderState= OrderState.Edited;
            }
            else if(state==2)
            {
                orderState= OrderState.Canceled;
            }
            else if (state == 3)
            {
                orderState = OrderState.ReadyToServe;
            }
            OrderDetailInKitchenWindow objOrderDetailInKitchenWindow = new OrderDetailInKitchenWindow(orderNo, tableNo,orderState);
            objOrderDetailInKitchenWindow.Owner = mainWindow;
            bool? dg=objOrderDetailInKitchenWindow.ShowDialog();
            if (dg == true)
            {
                FilterTextBox.Text = "";
                LoadOrders();
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

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            FilterTextBox.Text = "";
            LoadOrders();
            this.Cursor = Cursors.Arrow;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DataRowView selectedRow = (DataRowView)btn.Tag;
            OrdersGridView.SelectedItem = selectedRow;
            Int64 orderNo = (Int64)selectedRow["OrderNo"];
            string tableNo = selectedRow["TableNo"].ToString();
            Byte state = (Byte)selectedRow["State"];

            AutoPrintPage printOrder = new AutoPrintPage();
            PrintDialog pdg = new PrintDialog();
            if (pdg.ShowDialog() == true)
            {
                Visual content = printOrder.GetContent(orderNo, tableNo, state,false);
                if (content != null)
                {
                    pdg.PrintVisual(content, "Print Order #"+orderNo.ToString());
                }

                string spName = "Update_OrderPrintState";
                object [] spParams = new object[1];
                spParams[0] = orderNo;
                try
                {
                    objSqlDatabase.ExecuteNonQuery( spName,spParams);
                    this.Cursor = Cursors.Wait;
                    FilterTextBox.Text = "";
                    LoadOrders();
                    this.Cursor = Cursors.Arrow;
                }
                catch (SqlException)
                { }
            }
        }
    }

}
