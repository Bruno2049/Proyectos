using System;
using System.Windows;
using Northwind.ViewModel;

namespace Northwind.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)DataContext; }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowCustomerDetails();
        }
    }
}
