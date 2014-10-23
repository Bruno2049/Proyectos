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
using System.Data.SqlClient;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string conStr = Restaurant.Properties.Settings.Default.ConnectionStr;
                string[] arrTemp = conStr.Split(';');
                txtServerName.Text = arrTemp[0].Split('=')[1];
                txtDatabaseName.Text = arrTemp[1].Split('=')[1];

                txtUserName.Text = arrTemp[2].Split('=')[1];
                pbPassword.Password = arrTemp[3].Split('=')[1];
                SliderTimeout.Value = Convert.ToDouble(arrTemp[4].Split('=')[1]);
            }
            catch (Exception) 
            {
            }
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            bool state = false;
            string errorText = "";
            int x = 0;
            if (!(txtServerName.Text.Length > 0))
            {
                errorText += "ServerName is required" + "\n";
                state = true;
            }
            if (!(txtDatabaseName.Text.Length > 0))
            {
                errorText += "DataBase Name is required" + "\n";
                state = true;
            }

            if (!(txtUserName.Text.Length > 0))
            {
                errorText += "UserName is required";
                state = true;
            }

            if (state == false)
            {
                try
                {
                    string conStr = "Data Source=" + txtServerName.Text + ";Initial Catalog=" + txtDatabaseName.Text + ";User Id=" + txtUserName.Text + ";Password=" + pbPassword.Password + ";Connection Timeout=" + SliderTimeout.Value.ToString() + "";
                    Restaurant.Properties.Settings.Default.ConnectionStr = conStr;
                    Restaurant.Properties.Settings.Default.Save();
                    Restaurant.Properties.Settings.Default.Reload();
                    MessageBox.Show("Config has changed successfully. You need to launch the application again.");
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(errorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            bool state = false;
            string errorText = "";
            int x = 0;
            if (!(txtServerName.Text.Length > 0))
            {
                errorText += "ServerName is required" + "\n";
                state = true;
            }
            if (!(txtDatabaseName.Text.Length > 0))
            {
                errorText += "DataBase Name is required" + "\n";
                state = true;
            }

            if (!(txtUserName.Text.Length > 0))
            {
                errorText += "UserName is required";
                state = true;
            }

            if (state == false)
            {
                string conStr = "Data Source=" + txtServerName.Text + ";Initial Catalog=" + txtDatabaseName.Text + ";User Id=" + txtUserName.Text + ";Password=" + pbPassword.Password + ";Connection Timeout=" + SliderTimeout.Value.ToString() + "";
                SqlConnection con = new SqlConnection(conStr);
                try
                {
                    con.Open();
                    con.Close();
                    MessageBox.Show(ErrorMessages.Default.ConnectionTestSucceed);
                }
                catch (SqlException)
                {
                    MessageBox.Show(ErrorMessages.Default.ConnectionTestFailed,"Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            else
            {
                MessageBox.Show(errorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }
    }
}
