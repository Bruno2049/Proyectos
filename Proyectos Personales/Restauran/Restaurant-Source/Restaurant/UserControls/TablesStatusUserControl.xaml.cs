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
using System.Linq;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for ProductAvailabilityUserControl.xaml
    /// </summary>
    public partial class TablesStatusUserControl : UserControl
    {
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        DataView tablesDataView;

        public TablesStatusUserControl()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (objDatabaseClass.CheckConnection())
            {
                GetAllTables();
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        ///This method populate the Tables Grid
        /// </summary>
        internal void GetAllTables()
        {
            this.Cursor = Cursors.Wait;
            if (objDatabaseClass.CheckConnection())
            {
            string spName = "Get_All_Tables";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                tablesDataView = ds.Tables[0].DefaultView;
                tablesDataView.Sort = "[State],TableNo,Capacity";
                TablesGridView.ItemsSource = tablesDataView;
                TablesGridView.SelectedIndex = -1;
                var query = from o in ds.Tables[0].AsEnumerable()
                            where o.Field<bool>("State") == false
                            select o.Table.Rows.Count;
                int temp = 0;
                temp = query.Count<int>();
                txbFreeTables.Text = temp.ToString();
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

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            GetAllTables();
            this.Cursor = Cursors.Arrow;
        }
    }
}
