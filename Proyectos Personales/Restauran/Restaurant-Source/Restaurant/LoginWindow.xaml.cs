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
using System.Windows.Shapes;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            txtUserName.Focus();
            this.Cursor = Cursors.Arrow;
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            bool state = false;
            string errorText = "";
            if (!(txtUserName.Text.Length > 0))
            {
                errorText = ErrorMessages.Default.UserNotValid;
                state = true;
            }
            else if (!(pbPassword.Password.Length > 0))
            {
                errorText = ErrorMessages.Default.UserNotValid;
                state = true;
            }
            else if (!(pbPassword.Password.Length >= 3))
            {
                errorText = ErrorMessages.Default.UserNotValid;
                state = true;
            }
            ////////
            if (state == false)
            {
                try
                {
                    string userName = txtUserName.Text;
                    string password = pbPassword.Password;
                    string encrypt_password = GlobalClass.GetMd5Hash(password);
                    CheckUser(userName, encrypt_password);
                }
                catch { }
                    
            }
            else
            {
                MessageBox.Show(errorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void CheckUser(string userName, string password)
        {
            this.Cursor = Cursors.Wait;

            //Defualt UserName and Password For Config
            //UserName=Config and Password=ConfigPass
            if (userName == "Config" & password == "096E62FD294491F0D2E9C007012C9E94")
            {
                txtUserName.Text = "";
                pbPassword.Password = "";
                //Shows Config Form
                ConfigWindow objConfigWindow = new ConfigWindow();
                objConfigWindow.Owner = this;
                bool dg = (bool)objConfigWindow.ShowDialog();
                if (dg == true)
                {
                    this.DialogResult = false;
                }
                else
                {
                    txtUserName.Focus();
                }
                objConfigWindow = null;
            }
            else
            {
                SqlDatabase objSqlDatabase = null;
                DatabaseClass objDatabaseClass = null;
                try
                {
                    objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
                    objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
                    if (objDatabaseClass.CheckConnection())
                    {
                        string spName = "Check_User";
                        object[] spParams = new object[2];
                        spParams[0] = userName;
                        spParams[1] = password;
                        DataSet ds = objSqlDatabase.ExecuteDataSet(spName, spParams);
                        if (ds.Tables[0].Rows.Count == 1)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["UserName"]) == userName & Convert.ToString(ds.Tables[0].Rows[0]["Password"]) == password)
                            {
                                // Waiter is not allowed to login into the Restaurant application
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["PermissionID"]) == 4)
                                {
                                    MessageBox.Show(ErrorMessages.Default.WaiterIsNotPermited, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    this.DialogResult = false;
                                }
                                else
                                {
                                    GlobalClass._UserName = userName;
                                    GlobalClass._Password = password;
                                    GlobalClass._FullName = Convert.ToString(ds.Tables[0].Rows[0]["FullName"]);
                                    GlobalClass._PermissionID = Convert.ToInt32(ds.Tables[0].Rows[0]["PermissionID"]);
                                    GlobalClass._UserID = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"]);
                                    GlobalClass._Today = Convert.ToDateTime(ds.Tables[0].Rows[0]["Today"]);

                                    txtUserName.Text = "";
                                    pbPassword.Password = "";
                                    //this.DialogResult = true;
                                    this.Hide();
                                    MainWindow objMainWindow = new MainWindow();
                                    bool? dg = objMainWindow.ShowDialog();
                                    if (dg == false)
                                    {
                                        objMainWindow.Close();
                                        Application.Current.Shutdown();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(ErrorMessages.Default.UserNotValid, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                pbPassword.Password = "";
                                txtUserName.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show(ErrorMessages.Default.UserNotValid, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            pbPassword.Password = "";
                            txtUserName.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.DialogResult = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ErrorMessages.Default.UnhandledException,"Error",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            this.Cursor = Cursors.Arrow;
        }
    }
}
