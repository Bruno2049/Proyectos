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
	/// Interaction logic for UsersWindow.xaml
	/// </summary>
	public partial class UsersWindow : Window
	{
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        Int32 _UserID = -1;

		public UsersWindow()
		{
            this.Cursor = Cursors.Wait;
            this.InitializeComponent();
            if (objDatabaseClass.CheckConnection())
            {
                GetPermissionTypes();
                GetAllUsers();
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
        ///This method populate the users Grid
        /// </summary>
        private void GetAllUsers()
        {
            string spName = "Get_All_Users";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                UsersGridView.ItemsSource = ds.Tables[0].DefaultView;
                UsersGridView.SelectedIndex = -1;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            catch (SqlException)
            { }
        }

        /// <summary>
        ///This method populate the permission combobox
        /// </summary>
        private void GetPermissionTypes()
        {
            string commandText = "SELECT PermissionID,Description FROM Permissions";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.Text, commandText);
                List<PersmissionClass> permisionsList = new List<PersmissionClass>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    permisionsList.Add(new PersmissionClass(Convert.ToInt32(ds.Tables[0].Rows[i]["PermissionID"]), ds.Tables[0].Rows[i]["Description"].ToString()));
                }
                ds = null;
                cmbPermission.ItemsSource = permisionsList;
                cmbPermission.DisplayMemberPath = "Permission";
                cmbPermission.SelectedValuePath = "PermissionID";
                cmbPermission.SelectedIndex = -1;
            }
            catch (SqlException)
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            txbBorderTitle.Text = "New User";
            cmbPermission.SelectedIndex = 0;
            BorderAddEdit.IsEnabled = true;

            _UserID = -1;
            txtFullName.Text = "";
            txtUserName.Text = "";
            pbPassword.Password = "";
            pbConfirmPassword.Password = "";
            pbPassword.IsEnabled = true;
            pbConfirmPassword.IsEnabled = true;
            cmbPermission.SelectedIndex = -1;
            UsersGridView.IsEnabled = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGridView.SelectedItems.Count > 0)
            {
                UsersGridView.IsEnabled = false;
                txbBorderTitle.Text = "Edit User";
                _UserID = Convert.ToInt32(((DataRowView)UsersGridView.SelectedItem)["UserID"]);
                txtFullName.Text = ((DataRowView)UsersGridView.SelectedItem)["FullName"].ToString();
                txtUserName.Text = ((DataRowView)UsersGridView.SelectedItem)["UserName"].ToString();
                Int16 userType = Convert.ToInt16(((DataRowView)UsersGridView.SelectedItem)["PermissionID"]);
                cmbPermission.SelectedValue = userType;
                pbPassword.IsEnabled = false;
                pbConfirmPassword.IsEnabled = false;
                pbPassword.Password = "123456";
                pbConfirmPassword.Password = "123456";
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
            if (UsersGridView.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(ErrorMessages.Default.ConfirmDelete, "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Delete_User";
                        object[] spParams = new object[1];
                        spParams[0] = Convert.ToInt32(((DataRowView)UsersGridView.SelectedItem)["UserID"]);
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                GetAllUsers();
                                Reset();
                            }
                        }
                        catch (SqlException)
                        {
                            //we check something
                            MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        /// <summary>
        ///This is for reseting the selected user password to 123
        /// </summary>
        private void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (UsersGridView.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(ErrorMessages.Default.ConfirmResetPassword, "Reset Password", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Reset_User_Passw0rd";
                        object[] spParams = new object[1];
                        spParams[0] = Convert.ToInt32(((DataRowView)UsersGridView.SelectedItem)["UserID"]);
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                MessageBox.Show(ErrorMessages.Default.ResetPassSucceed);
                            }
                        }
                        catch (SqlException)
                        { }
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
            if (txbBorderTitle.Text == "New User")
            {
                string errorText = "";
                bool state = false;
                if (txtFullName.Text.Length < 2)
                {
                    errorText += ErrorMessages.Default.UserFullNameEmpty + "\n";
                    state = true;
                }
                if (txtUserName.Text.Length < 1)
                {
                    errorText += ErrorMessages.Default.UserNameEmpty + "\n";
                    state = true;
                }
                if (pbPassword.Password.Length < 3)
                {
                    errorText += ErrorMessages.Default.PasswordEmpty + "\n";
                    state = true;
                }
                else if (!CheckPassword(pbPassword.Password, pbConfirmPassword.Password))
                {
                    errorText += ErrorMessages.Default.ConflictPassword + "\n";
                    state = true;
                }
                if (!(cmbPermission.SelectedIndex > -1))
                {
                    errorText += ErrorMessages.Default.PermissionNotChoosed;
                    state = true;
                }
                if (state == false)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Create_User";
                        object[] spParams = new object[4];
                        spParams[0] = txtFullName.Text;
                        spParams[1] = txtUserName.Text;
                        spParams[2] = GlobalClass.GetMd5Hash(pbPassword.Password);
                        spParams[3] = Convert.ToInt16(cmbPermission.SelectedValue);
                        try
                        {
                            int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                            if (result > 0)
                            {
                                Reset();
                                GetAllUsers();
                                MessageBox.Show(string.Format(ErrorMessages.Default.RecordCreatedSuccessfully,"User"));
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627)
                            {
                                MessageBox.Show(string.Format(ErrorMessages.Default.RepeatedRecord,"User Name"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            else if (txbBorderTitle.Text == "Edit User")
            {
                string errorText = "";
                bool state = false;
                if (txtFullName.Text.Length < 2)
                {
                    errorText += ErrorMessages.Default.UserFullNameEmpty + "\n";
                    state = true;
                }
                if (txtUserName.Text.Length < 1)
                {
                    errorText += ErrorMessages.Default.UserNameEmpty + "\n";
                    state = true;
                }
                if (!(cmbPermission.SelectedIndex > -1))
                {
                    errorText += ErrorMessages.Default.PermissionNotChoosed;
                    state = true;
                }
                if (state == false)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        if (_UserID != -1)
                        {
                            string spName = "Update_User";
                            object[] spParams = new object[4];
                            spParams[0] = _UserID;
                            spParams[1] = txtFullName.Text;
                            spParams[2] = txtUserName.Text;
                            spParams[3] = Convert.ToInt16(cmbPermission.SelectedValue);
                            try
                            {
                                int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                                if (result > 0)
                                {
                                    Reset();
                                    GetAllUsers();
                                    MessageBox.Show(ErrorMessages.Default.EditedSuccessfully);
                                }
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number == 2627)
                                {
                                    MessageBox.Show(string.Format(ErrorMessages.Default.RepeatedRecord,"User Name"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        /// <summary>
        ///Compares Password with Confirm password
        ///returns True when both are the same
        ///returns false when they are not the same
        /// </summary>
        private bool CheckPassword(string pass1, string pass2)
        {
            if (pass1 == pass2)
            {
                return (true);
            }
            else
            {
                return (false);
            }
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
            _UserID = -1;
            txtFullName.Text = "";
            txtUserName.Text = "";
            pbPassword.Password = "";
            pbConfirmPassword.Password = "";
            cmbPermission.SelectedIndex = -1;
            BorderAddEdit.IsEnabled = false;
            UsersGridView.IsEnabled = true;
        }
	}

    class PersmissionClass
    {
        public PersmissionClass(int permisionID, string permission)
        {
            this._permissionID = permisionID;
            this._permission = permission;
        }

        private int _permissionID = -1;
        public int PermissionID
        {
            get { return _permissionID; }
            set { _permissionID = value; }
        }

        private string _permission = "";
        public string Permission
        {
            get { return _permission; }
            set { _permission = value; }
        }
    }
}