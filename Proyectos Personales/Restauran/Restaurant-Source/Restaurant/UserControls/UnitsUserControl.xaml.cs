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
	/// Interaction logic for UnitsUserControl.xaml
	/// </summary>
	public partial class UnitsUserControl
	{
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);

		public UnitsUserControl()
		{
            this.Cursor = Cursors.Wait;
            this.InitializeComponent();
            if (objDatabaseClass.CheckConnection())
            {
                GetAllUnits();
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
		}

        /// <summary>
        ///This method populate the Units Grid
        /// </summary>
        private void GetAllUnits()
        {
            string spName = "Get_All_Units";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                UnitsGridView.ItemsSource = ds.Tables[0].DefaultView;
                UnitsGridView.SelectedIndex = -1;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            catch (SqlException)
            { }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            txbBorderTitle.Text = "New Unit";
            BorderAddEdit.IsEnabled = true;

            txtUnitName.IsEnabled = true;
            txtShortName.Text = "";
            txtUnitName.Text = "";
            UnitsGridView.IsEnabled = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (UnitsGridView.SelectedItems.Count > 0)
            {
                UnitsGridView.IsEnabled = false;
                txbBorderTitle.Text = "Edit Unit";
                txtUnitName.IsEnabled = false;
                txtUnitName.Text = ((DataRowView)UnitsGridView.SelectedItem)["UnitName"].ToString();
                txtShortName.Text = ((DataRowView)UnitsGridView.SelectedItem)["ShortName"].ToString();
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
            if (UnitsGridView.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(ErrorMessages.Default.ConfirmDelete, "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Delete_Unit";
                        object[] spParams = new object[1];
                        spParams[0] = ((DataRowView)UnitsGridView.SelectedItem)["UnitName"].ToString();
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                GetAllUnits();
                                Reset();
                            }
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show(string.Format(ErrorMessages.Default.ItemCanNotBeDeleted,"unit"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (txbBorderTitle.Text == "New Unit")
            {
                string errorText = "";
                bool state = false;
                if (string.IsNullOrEmpty(txtUnitName.Text))
                {
                    errorText += ErrorMessages.Default.UnitNameEmpty + "\n";
                    state = true;
                }
                if (state == false)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Create_Unit";
                        object[] spParams = new object[2];
                        spParams[0] = txtUnitName.Text;
                        spParams[1] = txtShortName.Text;
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                Reset();
                                GetAllUnits();
                                MessageBox.Show(string.Format(ErrorMessages.Default.RecordCreatedSuccessfully, "Unit"));
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627)
                            {
                                MessageBox.Show(string.Format(ErrorMessages.Default.RepeatedRecord, "Unit Name"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            else if (txbBorderTitle.Text == "Edit Unit")
            {
                string errorText = "";
                bool state = false;
                if (string.IsNullOrEmpty(txtUnitName.Text))
                {
                    errorText += ErrorMessages.Default.UnitNameEmpty + "\n";
                    state = true;
                }
                if (state == false)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                            string spName = "Update_Unit";
                            object[] spParams = new object[2];
                            spParams[0] = txtUnitName.Text;
                            spParams[1] = txtShortName.Text;
                            try
                            {
                                int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                                if (result > 0)
                                {
                                    Reset();
                                    GetAllUnits();
                                    MessageBox.Show(ErrorMessages.Default.EditedSuccessfully);
                                }
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number == 2627)
                                {
                                    MessageBox.Show(string.Format(ErrorMessages.Default.RepeatedRecord, "Unit Name"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            txtUnitName.IsEnabled = true;
            txtShortName.Text = "";
            txtUnitName.Text = "";
            BorderAddEdit.IsEnabled = false;
            UnitsGridView.IsEnabled = true;
        }
	}
}