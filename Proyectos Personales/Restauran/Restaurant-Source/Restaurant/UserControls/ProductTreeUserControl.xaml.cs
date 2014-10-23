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
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using Restaurant.Classes;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for ProductTreeUserControl.xaml
    /// </summary>
    public partial class ProductTreeUserControl : UserControl
    {
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);

        public ProductTreeUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshProductsList();
        }

        /// <summary>
        /// Popolates the Product TreeView
        /// </summary>
        public void RefreshProductsList()
        {
            GetAllGroups();
        }

        /// <summary>
        ///This method populate the Group Combobox
        /// </summary>
        private void GetAllGroups()
        {
            cmbGroup.ItemsSource = null;
            string spName = "Get_All_Groups";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                List<GroupClass> groupList = new List<GroupClass>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    groupList.Add(new GroupClass(Convert.ToInt32(ds.Tables[0].Rows[i]["GroupID"]), ds.Tables[0].Rows[i]["GroupName"].ToString()));
                }
                ds = null;
                cmbGroup.ItemsSource = groupList;
                cmbGroup.DisplayMemberPath = "GroupName";
                cmbGroup.SelectedValuePath = "GroupID";
                cmbGroup.SelectedIndex = -1;
            }
            catch (SqlException)
            { }
        }

        /// <summary>
        ///This method populate the Product Grid
        /// </summary>
        private void GetProducts(int groupID)
        {
            try
            {
                string spName;
                if (groupID != -1)
                {
                    spName = "Get_All_Products_By_GroupID";
                    object[] spParams = new object[1];
                    spParams[0] = groupID;
                    DataSet ds = objSqlDatabase.ExecuteDataSet(spName, spParams);
                    ProductsGridView.ItemsSource = ds.Tables[0].DefaultView;
                    ProductsGridView.SelectedIndex = -1;
                }
            }
            catch (SqlException)
            { }
        }

        private void cmbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbGroup.SelectedIndex > -1)
            {
                int groupID = ((GroupClass)cmbGroup.SelectedItem).GroupID;
                this.Cursor = Cursors.Wait;
                GetProducts(groupID);
                this.Cursor = Cursors.Arrow;
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGridView.Items.Count > 0)
            {
                string groupName = ((GroupClass)cmbGroup.SelectedItem).GroupName;
                DataTable dtProducts = ((DataView)ProductsGridView.ItemsSource).Table;
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
                ReportParameter[] arrParams = new ReportParameter[4];
                arrParams[0] = new ReportParameter("rpm_GroupName", groupName);
                arrParams[1] = new ReportParameter("rpm_Date", GlobalClass.GetCurrentDateTime().ToShortDateString());
                arrParams[2] = new ReportParameter("rpm_AddressLine1", AddressLine1);
                arrParams[3] = new ReportParameter("rpm_AddressLine2", AddressLine2);
                string reportPath = Environment.CurrentDirectory + "\\ReportSrc";
                ReportPrintClass objReportPrintClass = new ReportPrintClass("PublicDataSet_Products", dtProducts, reportPath + "\\Report_Menu.rdlc", arrParams, 8.27, 11.69, 0.5, 0.5, 0.2, 0.2);

                PrinterSettings ps = new PrinterSettings();
                PrinterSettings.StringCollection printersList = PrinterSettings.InstalledPrinters;
                string[] arrPrinters = new string[printersList.Count];
                for (int i = 0; i < printersList.Count; i++)
                {
                    arrPrinters[i] = printersList[i];

                }
                CustomPrintDialog objCustomPrintDialog = new CustomPrintDialog(arrPrinters);
                if (objCustomPrintDialog.ShowDialog() == true)
                {
                    objReportPrintClass.Print(objCustomPrintDialog.printerName, 1);
                }
                objReportPrintClass.Dispose();
            }
            else
            {
                MessageBox.Show("There is no product to print!","", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
