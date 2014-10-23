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
    public partial class CustomPrintDialog : Window
    {
        public string printerName = "";
        public CustomPrintDialog(string[] printerLists)
        {
            InitializeComponent();
            cmbPrinter.ItemsSource=printerLists;
            cmbPrinter.SelectedIndex = 0;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPrinter.SelectedIndex > -1)
            {
                printerName = cmbPrinter.SelectedItem.ToString();
                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
            }
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
        }

    }
}
