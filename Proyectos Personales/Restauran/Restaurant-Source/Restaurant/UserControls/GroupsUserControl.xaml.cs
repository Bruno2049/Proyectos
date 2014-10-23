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
	/// Interaction logic for GroupsUserControl.xaml
	/// </summary>
	public partial class GroupsUserControl
	{
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        int _groupID=-1;

		public GroupsUserControl()
		{
            this.Cursor = Cursors.Wait;
            this.InitializeComponent();
            if (objDatabaseClass.CheckConnection())
            {
                GetAllGroups();
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
		}

        /// <summary>
        ///This method populate the Groups Grid
        /// </summary>
        private void GetAllGroups()
        {
            string spName = "Get_All_Groups";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                GroupsGridView.ItemsSource = ds.Tables[0].DefaultView;
                GroupsGridView.SelectedIndex = -1;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            catch (SqlException)
            { }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            txbBorderTitle.Text = "New Group";
            BorderAddEdit.IsEnabled = true;

            txtGroupName.Text = "";
            GroupsGridView.IsEnabled = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsGridView.SelectedItems.Count > 0)
            {
                GroupsGridView.IsEnabled = false;
                txbBorderTitle.Text = "Edit Group";
                _groupID=Convert.ToInt32(((DataRowView)GroupsGridView.SelectedItem)["GroupID"]);
                txtGroupName.Text = ((DataRowView)GroupsGridView.SelectedItem)["GroupName"].ToString();
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
            if (GroupsGridView.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(ErrorMessages.Default.ConfirmDelete, "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Delete_Group";
                        object[] spParams = new object[1];
                        spParams[0] =Convert.ToInt32(((DataRowView)GroupsGridView.SelectedItem)["GroupID"]);
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                GetAllGroups();
                                Reset();
                            }
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show(string.Format(ErrorMessages.Default.ItemCanNotBeDeleted, "group"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (txbBorderTitle.Text == "New Group")
            {
                string errorText = "";
                bool state = false;
                if (string.IsNullOrEmpty(txtGroupName.Text))
                {
                    errorText += string.Format(ErrorMessages.Default.FieldEmpty,"Group Name");
                    state = true;
                }
                if (state == false)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Create_Group";
                        object[] spParams = new object[1];
                        spParams[0] = txtGroupName.Text;
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                Reset();
                                GetAllGroups();
                                MessageBox.Show(string.Format(ErrorMessages.Default.RecordCreatedSuccessfully, "Group"));
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627)
                            {
                                MessageBox.Show(string.Format(ErrorMessages.Default.RepeatedRecord, "Group Name"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            else if (txbBorderTitle.Text == "Edit Group")
            {
                string errorText = "";
                bool state = false;
                if (string.IsNullOrEmpty(txtGroupName.Text))
                {
                    errorText += string.Format(ErrorMessages.Default.FieldEmpty, "Group Name");
                    state = true;
                }
                if (state == false)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Update_Group";
                        object[] spParams = new object[2];
                        spParams[0] = _groupID;
                        spParams[1] = txtGroupName.Text;
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                Reset();
                                GetAllGroups();
                                MessageBox.Show(ErrorMessages.Default.EditedSuccessfully);
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627)
                            {
                                MessageBox.Show(string.Format(ErrorMessages.Default.RepeatedRecord, "Group Name"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            txtGroupName.Text = "";
            BorderAddEdit.IsEnabled = false;
            GroupsGridView.IsEnabled = true;
        }
	}
}