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
using System.IO;
using System.Data.SqlClient;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        string logoPath = "";

        public SettingsWindow()
        {
            InitializeComponent();
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
            DataTable dt=GlobalClass.GetRestaurantInfo();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    txtRestaurantName.Text = dt.Rows[0]["RestaurantName"].ToString();
                    txtPhoneNumber.Text = dt.Rows[0]["PhoneNumber"].ToString();
                    txtWebsite.Text = dt.Rows[0]["WebsiteURL"].ToString();
                    txtEmail.Text = dt.Rows[0]["Email"].ToString();
                    txtAddress.Text = dt.Rows[0]["Address"].ToString();

                    try
                    {
                        if (dt.Rows[0]["Logo"] != DBNull.Value)
                        {
                            byte[] arr = (byte[])dt.Rows[0]["Logo"];
                            MemoryStream ms = new MemoryStream(arr);
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.StreamSource = ms;
                            bitmap.EndInit();
                            imgLogo.Source = bitmap;
                        }
                    }
                    catch { }
                }
            }
        }

        private void btnBrowseLogo_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "Image files|*.jpg;*.png;*.gif";
            System.Windows.Forms.DialogResult dgr = ofd.ShowDialog();
            if (dgr == System.Windows.Forms.DialogResult.OK)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(ofd.FileName));
                if (bitmap.Width <= 80 && bitmap.Height <= 80)
                {
                    imgLogo.Source = bitmap;
                    logoPath = ofd.FileName;
                }
                else
                {
                    MessageBox.Show("Logo image should have the size of 80*80 or less!","", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            if (txtRestaurantName.Text != "" && imgLogo.Source != null)
            {
                string spName = "Update_Settings";
                if (objDatabaseClass.CheckConnection())
                {
                    object[] spParams = new object[6];
                    spParams[0] = txtRestaurantName.Text;
                    spParams[1] = txtAddress.Text;
                    spParams[2] = txtPhoneNumber.Text;
                    spParams[3] = txtWebsite.Text;
                    spParams[4] = txtEmail.Text;
                    if (logoPath != "")
                    {
                        FileStream fsLogo = new FileStream(logoPath, FileMode.Open, FileAccess.Read);
                        FileInfo objFileInfoLogo = new FileInfo(logoPath);
                        BinaryReader brLogo = new BinaryReader(fsLogo);
                        long lengthLogo = objFileInfoLogo.Length;
                        byte[] arrImgLogo = new byte[Convert.ToInt32(lengthLogo)];
                        arrImgLogo = brLogo.ReadBytes(Convert.ToInt32(lengthLogo));
                        fsLogo.Flush();
                        brLogo.Close();
                        fsLogo.Close();
                        spParams[5] = arrImgLogo;
                    }
                    else
                    {
                        spParams[5] = DBNull.Value;
                    }
                    try
                    {
                        int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
                        if (result > 0)
                        {
                            MessageBox.Show("Settings Saved Successfully!");
                            this.DialogResult = false;
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
                MessageBox.Show(string.Format(ErrorMessages.Default.FieldEmpty,"Restaurant Name")+"\n"+
                    string.Format(ErrorMessages.Default.FieldEmpty, "Logo"),"", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Cursor = Cursors.Arrow;
        }
    }
}
