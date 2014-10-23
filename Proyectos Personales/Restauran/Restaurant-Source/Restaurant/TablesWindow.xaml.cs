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
	/// Interaction logic for TablesWindow.xaml
	/// </summary>
	public partial class TablesWindow : Window
	{
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);

		public TablesWindow()
		{
            this.Cursor = Cursors.Wait;
            this.InitializeComponent();
            if (objDatabaseClass.CheckConnection())
            {
                GetAllTables();
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
            }
            this.Cursor = Cursors.Arrow;
		}

        /// <summary>
        ///When the user close the window this event will be fired
        /// </summary>
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        ///This event is for moving the window
        ///</summary>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        ///This method populate the Tables Grid
        /// </summary>
        private void GetAllTables()
        {
            string spName = "Get_All_Tables";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                ds.Tables[0].DefaultView.Sort = "TableNo";
                TablesGridView.ItemsSource = ds.Tables[0].DefaultView;
                TablesGridView.SelectedIndex = -1;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            catch (SqlException)
            { }
        }

        private void btnNewTable_Click(object sender, RoutedEventArgs e)
        {
            txbBorderTitle.Text = "New Table";
            BorderAddEdit.IsEnabled = true;
            txtTableNo.Focus();
            txtTableNo.Text = "";
            txtTableNo.IsEnabled = true;
            txtCapacity.Text = "1";
            txtDescription.Text = "";
            TablesGridView.IsEnabled = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TablesGridView.SelectedItems.Count > 0)
            {
                TablesGridView.IsEnabled = false;
                txbBorderTitle.Text = "Edit Table";
                txtTableNo.IsEnabled = false;
                txtTableNo.Text = ((DataRowView)TablesGridView.SelectedItem)["TableNo"].ToString();
                txtCapacity.Text = ((DataRowView)TablesGridView.SelectedItem)["Capacity"].ToString();
                txtDescription.Text = ((DataRowView)TablesGridView.SelectedItem)["Description"].ToString();
                
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
            if (TablesGridView.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(ErrorMessages.Default.ConfirmDelete, "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Delete_Table";
                        object[] spParams = new object[1];
                        spParams[0] = ((DataRowView)TablesGridView.SelectedItem)["TableNo"].ToString();
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                GetAllTables();
                                Reset();
                            }
                        }
                        catch (SqlException)
                        {

                            MessageBox.Show(string.Format(ErrorMessages.Default.ItemCanNotBeDeleted,"table"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (txbBorderTitle.Text == "New Table")
            {
                string errorText = "";
                bool state = false;
                if (string.IsNullOrEmpty(txtTableNo.Text))
                {
                    errorText += ErrorMessages.Default.TableNoEmpty + "\n";
                    state = true;
                }
                if (string.IsNullOrEmpty(txtCapacity.Text))
                {
                    errorText += ErrorMessages.Default.TableCapacityEmpty;
                    state = true;
                }
                if (state == false)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Create_Table";
                        object[] spParams = new object[3];
                        spParams[0] = txtTableNo.Text;
                        spParams[1] =Convert.ToInt16(txtCapacity.Text);
                        spParams[2] = txtDescription.Text;
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                Reset();
                                GetAllTables();
                                MessageBox.Show(string.Format(ErrorMessages.Default.RecordCreatedSuccessfully,"Table"));
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627)
                            {
                                MessageBox.Show(string.Format(ErrorMessages.Default.RepeatedRecord,"TableNo"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            else if (txbBorderTitle.Text == "Edit Table")
            {
                string errorText = "";
                bool state = false;
                if (string.IsNullOrEmpty(txtTableNo.Text))
                {
                    errorText += ErrorMessages.Default.TableNoEmpty + "\n";
                    state = true;
                }
                if (string.IsNullOrEmpty(txtCapacity.Text))
                {
                    errorText += ErrorMessages.Default.TableCapacityEmpty;
                    state = true;
                }
                if (state == false)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                            string spName = "Update_Table";
                            object[] spParams = new object[3];
                            spParams[0] = txtTableNo.Text;
                            spParams[1] =Convert.ToInt16(txtCapacity.Text);
                            spParams[2] = txtDescription.Text;
                            try
                            {
                                int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                                if (result > 0)
                                {
                                    Reset();
                                    GetAllTables();
                                    MessageBox.Show(ErrorMessages.Default.EditedSuccessfully);
                                }
                            }
                            catch (SqlException)
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

        private void txtCapacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int a = 1;
            if (!int.TryParse(txtCapacity.Text, out a))
            {
                txtCapacity.Text = "1";
                txtCapacity.SelectAll();
            }
            else
            {
                int temp = Convert.ToInt32(txtCapacity.Text);
                if (temp <= 0)
                {
                    txtCapacity.Text = "1";
                    txtCapacity.SelectAll();
                }
                if (temp > 255)
                {
                    txtCapacity.Text = "255";
                    txtCapacity.SelectAll();
                }
            }
        }

        /// <summary>
        ///Reset the textboxes and other controls after save or cancel functions
        /// </summary>
        private void Reset()
        {
            txbBorderTitle.Text = "";
            txtTableNo.Text = "";
            txtTableNo.IsEnabled = true;
            txtCapacity.Text = "1";
            txtDescription.Text = "";
            BorderAddEdit.IsEnabled = false;
            TablesGridView.IsEnabled = true;
        }
 
	}
}