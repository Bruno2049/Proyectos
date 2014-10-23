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
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using Restaurant.Classes;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for SellReportWindow.xaml
    /// </summary>
    public partial class SellReportWindow : Window
    {
        // These are global object which will be use to communicate with the 
        // database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        DateTime? dtFromDate;
        DateTime? dtToDate;

        public SellReportWindow()
        {
            InitializeComponent();
            datePickerFromDate.SelectedDate = DateTime.Now;
            datePickerToDate.SelectedDate = DateTime.Now;
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnShowResult_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (datePickerFromDate.SelectedDate != null && datePickerToDate != null)
            {
                if (datePickerFromDate.SelectedDate <= datePickerToDate.SelectedDate)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        dtFromDate = datePickerFromDate.SelectedDate;
                        dtToDate = datePickerToDate.SelectedDate;
                        string spName = "Report_GetOrdersList";
                        object[] spParams = new object[2];
                        spParams[0] = datePickerFromDate.SelectedDate;
                        spParams[1] = datePickerToDate.SelectedDate;
                        try 
                        {
                            DataSet ds=objSqlDatabase.ExecuteDataSet(spName, spParams);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    ResultGridView.ItemsSource = ds.Tables[0].DefaultView;
                                }
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
                    MessageBox.Show("To Date cannot be less that From Date!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    datePickerToDate.Focus();
                }
            }
            else
            {
                MessageBox.Show("To Date and From Date should be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor=Cursors.Arrow;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (ResultGridView.Items.Count > 0)
            {
                DataTable dtOrders = ((DataView)ResultGridView.ItemsSource).Table;
                DataTable dtRestaurantInfo = GlobalClass.GetRestaurantInfo();
                string AddressLine1 = "";
                string AddressLine2 = "";
                if (dtRestaurantInfo != null)
                {
                    if (dtRestaurantInfo.Rows.Count > 0)
                    {
                        AddressLine1 = dtRestaurantInfo.Rows[0]["RestaurantName"].ToString() + ", " + dtRestaurantInfo.Rows[0]["Address"].ToString();
                        AddressLine2 = "Phone: " + dtRestaurantInfo.Rows[0]["PhoneNumber"].ToString() + "    " + dtRestaurantInfo.Rows[0]["WebsiteURL"].ToString() + "    " + dtRestaurantInfo.Rows[0]["Email"].ToString();
                    }
                }
                ReportParameter[] arrParams = new ReportParameter[4];
                arrParams[0] = new ReportParameter("rpm_FromDate", dtFromDate.Value.ToShortDateString());
                arrParams[1] = new ReportParameter("rpm_ToDate", dtToDate.Value.ToShortDateString());
                arrParams[2] = new ReportParameter("rpm_AddressLine1", AddressLine1);
                arrParams[3] = new ReportParameter("rpm_AddressLine2", AddressLine2);
                string reportPath = Environment.CurrentDirectory + "\\ReportSrc";
                ReportPrintClass objReportPrintClass = new ReportPrintClass("PublicDataSet_OrdersList_SellReport", dtOrders, reportPath + "\\Report_Sell.rdlc", arrParams, 8.27, 11.69, 0.5, 0.5, 0.2, 0.2);

                PrinterSettings ps = new PrinterSettings();
                PrinterSettings.StringCollection printersList = PrinterSettings.InstalledPrinters;
                string[] arrPrinters = new string[printersList.Count];
                for (int i = 0; i < printersList.Count; i++)
                {
                    arrPrinters[i] = printersList[i];

                }
                CustomPrintDialog objCustomPrintDialog = new CustomPrintDialog(arrPrinters);
                if (objCustomPrintDialog.ShowDialog() == true)
                {
                    objReportPrintClass.Print(objCustomPrintDialog.printerName, 1);
                }
                objReportPrintClass.Dispose();
            }
            else
            {
                MessageBox.Show("There is no item to print!","", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
