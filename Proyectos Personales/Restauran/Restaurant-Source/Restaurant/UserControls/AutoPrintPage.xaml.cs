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
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Restaurant.UserControls
{
    /// <summary>
    /// Interaction logic for AutoPrintPage.xaml
    /// </summary>
    public partial class AutoPrintPage : UserControl
    {
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);

        public AutoPrintPage()
        {
            InitializeComponent();
        }

        internal Visual GetContent(Int64 orderNo, string tableNo, byte state, bool isRepeated)
        {
            if (isRepeated)
            {
                txbRepeatedPrint.Visibility = Visibility.Visible;
            }
            Visual returnContent = null;
            DataTable dt = OrderContent(orderNo);
            if (dt != null)
            {
                if (state == 0)
                {
                    txbTitle.Text = "New Order";
                    EditAmountColumn.Width = 0;
                }
                else if (state == 1)
                {
                    txbTitle.Text = "Edited Order";
                    EditAmountColumn.Width = 60;
                }
                else if (state == 2)
                {
                    txbTitle.Text = "Canceled Order";
                    EditAmountColumn.Width = 0;
                    CancelSign.Visibility = Visibility.Visible;
                }
                txbOrderNo.Text = orderNo.ToString();
                txbTableNo.Text = tableNo;
                txbPrintDateTime.Text = DateTime.Now.ToShortDateString()+" "+ DateTime.Now.ToLongTimeString();
                lstContent.ItemsSource = dt.DefaultView;
                lstContent.SelectedIndex = -1;
                mainBorder.Width = 300;
                returnContent = mainBorder;
            }
            return returnContent;
        }

        internal Visual GetContent(Int64 orderNo, string tableNo, OrderState state, bool isRepeated)
        {
            if (isRepeated)
            {
                txbRepeatedPrint.Visibility = Visibility.Visible;
            }
            Visual returnContent = null;
            DataTable dt = OrderContent(orderNo);
            if (dt != null)
            {
                if (state == OrderState.New)
                {
                    txbTitle.Text = "New Order";
                    EditAmountColumn.Width = 0;
                }
                else if (state == OrderState.Edited)
                {
                    txbTitle.Text = "Edited Order";
                    EditAmountColumn.Width = 60;
                }
                else if (state ==  OrderState.Canceled)
                {
                    txbTitle.Text = "Canceled Order";
                    EditAmountColumn.Width = 0;
                    CancelSign.Visibility = Visibility.Visible;
                }
                txbOrderNo.Text = orderNo.ToString();
                txbTableNo.Text = tableNo;
                txbPrintDateTime.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                lstContent.ItemsSource = dt.DefaultView;
                lstContent.SelectedIndex = -1;
                mainBorder.Width = 300;
                returnContent = mainBorder;
            }
            return returnContent;
        }

        private DataTable OrderContent(Int64 _orderNo)
        {
            DataTable dt = null;
            string spName = "Get_Order_Detail_For_Auto_Print";
            object[] spParams = new object[1];
            spParams[0] = _orderNo;
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(spName, spParams);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (SqlException)
            { }
            return dt;
        }
    }
}
