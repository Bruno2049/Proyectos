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

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        #region These are global object which will be use to communicate with the database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        #endregion

        string _OldPassword = GlobalClass._Password;
        Int32 _UserID = GlobalClass._UserID;

        public ChangePasswordWindow()
        {
            InitializeComponent();
            pbCurrentPassword.Focus();
        }

        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string errorText = "";
            if (GlobalClass.GetMd5Hash(pbCurrentPassword.Password) != _OldPassword)
            {
                errorText += ErrorMessages.Default.CurrentPasswordNotCorrect+ "\n";
            }
            if (pbNewPassword.Password != pbConfirmNewPassword.Password)
            {
                errorText += ErrorMessages.Default.ConflictPassword+ "\n";
            }
            if (pbNewPassword.Password.Length < 3)
            {
                errorText += ErrorMessages.Default.PasswordEmpty + "\n";
            }
            if (errorText=="")
            {
                MessageBoxResult msgResult=MessageBox.Show(ErrorMessages.Default.ConfirmChangePassword,"", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (msgResult == MessageBoxResult.Yes)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        string newPass = GlobalClass.GetMd5Hash(pbNewPassword.Password);
                        string spName = "UPDATE_USER_PASS";
                        object[] spParams = new object[2];
                        spParams[0] = _UserID;
                        spParams[1] = newPass;
                        try
                        {
                            if (objSqlDatabase.ExecuteNonQuery(spName, spParams) > 0)
                            {
                                GlobalClass._Password = newPass;
                                MessageBox.Show(string.Format(ErrorMessages.Default.ActionSucceed,"Change Password"));
                                this.DialogResult = true;
                            }
                        }
                        catch
                        {
                            MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessages.Default.NoConnection,"Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.DialogResult = false;
                    }
                }
            }
            else
            {
                MessageBox.Show(errorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
