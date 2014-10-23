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
using System.Timers;


namespace Restaurant
{
	/// <summary>
	/// Interaction logic for ProductsUserControl.xaml
	/// </summary>
	public partial class ProductsUserControl
	{
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        int _productID = -1;

		public ProductsUserControl()
		{
            this.Cursor = Cursors.Wait;
            this.InitializeComponent();
            if (objDatabaseClass.CheckConnection())
            {
                GetAllGroups();
                GetAllUnits();
                GetAllProducts();
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
        ///This method populate the Unit Combobox
        /// </summary>
        private void GetAllUnits()
        {
            cmbUnit.Items.Clear();
            string spName = "Get_All_Units";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cmbUnit.Items.Add(ds.Tables[0].Rows[i]["UnitName"].ToString());
                }
                ds = null;
                cmbUnit.SelectedIndex = -1;
            }
            catch (SqlException)
            { }
        }

        public void Refresh()
        {
            this.Cursor = Cursors.Wait;
            if (objDatabaseClass.CheckConnection())
            {
                GetAllGroups();
                GetAllUnits();
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        ///This method populate the Product Grid
        /// </summary>
        private void GetAllProducts()
        {
            string spName = "Get_All_Products";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                ProductsGridView.ItemsSource = ds.Tables[0].DefaultView;
                ProductsGridView.SelectedIndex = -1;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            catch (SqlException)
            { }
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal a = 0.00m;
            if (!decimal.TryParse(txtPrice.Text, out a))
            {
                txtPrice.Text = "0.00";
                txtPrice.SelectAll();
            }
            else
            {
                decimal temp = Convert.ToDecimal(txtPrice.Text);
                if (temp <= 0)
                {
                    txtPrice.Text = "0.00";
                    txtPrice.SelectAll();
                }
                if (temp > 999999999999999999.99m)
                {
                    txtPrice.Text = "999999999999999999.99";
                    txtPrice.SelectAll();
                }
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            txbBorderTitle.Text = "New Product";
            cmbGroup.SelectedIndex = -1;
            cmbUnit.SelectedIndex = -1;
            BorderAddEdit.IsEnabled = true;

            txtProductName.Text = "";
            cmbGroup.SelectedIndex = -1;
            cmbUnit.SelectedIndex = -1;
            txtPrice.Text = "0.00";
            ProductsGridView.IsEnabled = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGridView.SelectedItems.Count > 0)
            {
                ProductsGridView.IsEnabled = false;
                txbBorderTitle.Text = "Edit Product";
                _productID = Convert.ToInt32(((DataRowView)ProductsGridView.SelectedItem)["ProductID"]);
                txtProductName.Text = ((DataRowView)ProductsGridView.SelectedItem)["ProductName"].ToString();
                txtPrice.Text = ((DataRowView)ProductsGridView.SelectedItem)["Price"].ToString();
                cmbUnit.SelectedItem= ((DataRowView)ProductsGridView.SelectedItem)["UnitName"].ToString();
                cmbGroup.SelectedValue =Convert.ToInt32(((DataRowView)ProductsGridView.SelectedItem)["GroupID"]);
                BorderAddEdit.IsEnabled = true;
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoRowSelected, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (ProductsGridView.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(ErrorMessages.Default.ConfirmDelete, "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Delete_Product";
                        object[] spParams = new object[1];
                        spParams[0] = Convert.ToInt32(((DataRowView)ProductsGridView.SelectedItem)["ProductID"]);
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                GetAllProducts();
                                Reset();
                            }
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show(string.Format(ErrorMessages.Default.ItemCanNotBeDeleted,"product"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoRowSelected, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (txbBorderTitle.Text == "New Product")
            {
                string errorText = "";
                bool state = false;
                if (string.IsNullOrEmpty(txtProductName.Text))
                {
                    errorText += string.Format(ErrorMessages.Default.FieldEmpty,"Product Name") + "\n";
                    state = true;
                }
                if (!(cmbGroup.SelectedIndex>-1))
                {
                    errorText += string.Format(ErrorMessages.Default.FieldEmpty,"Group") + "\n";
                    state = true;
                }
                if (!(cmbUnit.SelectedIndex > -1))
                {
                    errorText += string.Format(ErrorMessages.Default.FieldEmpty, "Unit") + "\n";
                    state = true;
                }
                if (state == false)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Create_Product";
                        object[] spParams = new object[4];
                        spParams[0] = txtProductName.Text;
                        spParams[1] = Convert.ToInt32(cmbGroup.SelectedValue);
                        spParams[2] = cmbUnit.SelectedItem.ToString();
                        spParams[3] = Convert.ToDecimal(txtPrice.Text);
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                Reset();
                                GetAllProducts();
                                MessageBox.Show(string.Format(ErrorMessages.Default.RecordCreatedSuccessfully, "Product"));
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627)
                            {
                                MessageBox.Show(string.Format(ErrorMessages.Default.RepeatedRecord, "Product Name"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(errorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (txbBorderTitle.Text == "Edit Product")
            {
                string errorText = "";
                bool state = false;
                if (string.IsNullOrEmpty(txtProductName.Text))
                {
                    errorText += string.Format(ErrorMessages.Default.FieldEmpty, "Product Name") + "\n";
                    state = true;
                }
                if (!(cmbGroup.SelectedIndex > -1))
                {
                    errorText += string.Format(ErrorMessages.Default.FieldEmpty, "Group") + "\n";
                    state = true;
                }
                if (!(cmbUnit.SelectedIndex > -1))
                {
                    errorText += string.Format(ErrorMessages.Default.FieldEmpty, "Unit") + "\n";
                    state = true;
                }
                if (state == false)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        if (_productID != -1)
                        {
                            string spName = "Update_Product";
                            object[] spParams = new object[5];
                            spParams[0] = _productID;
                            spParams[1] = txtProductName.Text;
                            spParams[2] = Convert.ToInt32(cmbGroup.SelectedValue);
                            spParams[3] =cmbUnit.SelectedItem.ToString();
                            spParams[4] = Convert.ToDecimal(txtPrice.Text);
                            try
                            {
                                int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                                if (result > 0)
                                {
                                    Reset();
                                    GetAllProducts();
                                    MessageBox.Show(ErrorMessages.Default.EditedSuccessfully);
                                }
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number == 2627)
                                {
                                    MessageBox.Show(string.Format(ErrorMessages.Default.RepeatedRecord,"Product Name"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(errorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        /// <summary>
        ///Reset the textboxes and other controls after save or cancel functions
        /// </summary>
        private void Reset()
        {
            txbBorderTitle.Text = "";
            txtProductName.Text = "";
            cmbGroup.SelectedIndex = -1;
            cmbUnit.SelectedIndex = -1;
            txtPrice.Text = "0.00";
            BorderAddEdit.IsEnabled = false;
            ProductsGridView.IsEnabled = true;
        }
	}

    class GroupClass
    {
        public GroupClass(int groupID, string groupName)
        {
            this._groupID = groupID;
            this._groupName = groupName;
        }

        private int _groupID = -1;
        public int GroupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }

        private string _groupName = "";
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }
    }
}