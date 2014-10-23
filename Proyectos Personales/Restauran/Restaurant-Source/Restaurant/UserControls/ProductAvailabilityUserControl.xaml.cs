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
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.SqlClient;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for ProductAvailabilityUserControl.xaml
    /// </summary>
    public partial class ProductAvailabilityUserControl : UserControl
    {
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        bool doFilter = false;
        DataView productDataView = null;
        DataTable productDataTable = null;

        public ProductAvailabilityUserControl()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (objDatabaseClass.CheckConnection())
            {
                GetAllGroups();
                doFilter = true;
                GetAllProducts();
                if (cmbGroups.Items.Count > 0)
                {
                    cmbGroups.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        ///This method populate the Group Combobox
        /// </summary>
        private void GetAllGroups()
        {
            cmbGroups.ItemsSource = null;
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
                cmbGroups.ItemsSource = groupList;
                cmbGroups.DisplayMemberPath = "GroupName";
                cmbGroups.SelectedValuePath = "GroupID";
            }
            catch (SqlException)
            { }
        }

        /// <summary>
        ///This method populate the Product Grid
        /// </summary>
        private void GetAllProducts()
        {
            string commandText = "SELECT ProductID, ProductName, GroupID, Availability FROM Products ORDER BY ProductName";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.Text, commandText);
                productDataTable = ds.Tables[0];
                productDataView = productDataTable.DefaultView;
                ProductsGridView.ItemsSource = productDataView;
                ProductsGridView.SelectedIndex = -1;
            }
            catch (SqlException)
            { }
        }

        private void cmbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doFilter)
            {
                int selectedGroupID=Convert.ToInt32(cmbGroups.SelectedValue);
                productDataView.RowFilter = "GroupID=" + selectedGroupID + "";
            }
        }

        private void cbtnAvailability_Checked(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            CheckBox cItem = (CheckBox)sender;
            DataRowView selectedRow = (DataRowView)cItem.Tag;
            ProductsGridView.SelectedItem = selectedRow;
            int productID=(int)selectedRow["ProductID"];
            if (cItem.IsChecked==true)
            {
                if (!UpdateAvailability(productID, true))
                {
                    cItem.IsChecked = false;
                }
            }
            else
            {
                if (!UpdateAvailability(productID, false))
                {
                    cItem.IsChecked = true;
                }
            }
            //DataRow dr = productDataTable.Rows.Find(selectedRow["ProductID"]);
            this.Cursor = Cursors.Arrow;
        }

        private bool UpdateAvailability(int productID, bool availability)
        {
            bool result = false;
            string spName = "Update_Product_Availability";
            object[] spParams = new object[2];
            spParams[0] = productID;
            spParams[1] = availability;
            try
            {
                int temp=objSqlDatabase.ExecuteNonQuery(spName, spParams);
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


    }
}
