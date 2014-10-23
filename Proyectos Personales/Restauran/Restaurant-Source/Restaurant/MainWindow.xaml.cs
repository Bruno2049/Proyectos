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
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Threading;
using System.Timers;
using Restaurant.UserControls;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Restaurant
{
    public partial class MainWindow : Window
    {
        #region Variables
        Thread printThread;
        public static System.Timers.Timer printTimer;
        static SqlDatabase objSqlDatabase;
        static DatabaseClass objDatabaseClass;
        TablesStatusUserControl tablesStatus;
        CashierOrdersListUserControl cashierOrdersList;
        ProductAvailabilityUserControl productAvailability;
        KitchenOrdersListUserControl kitchenOrdersList;
        DispatcherTimer refreshTimer;
        #endregion

        public MainWindow()
        {
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
            objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
            double interval = Restaurant.Properties.Settings.Default.RefreshInterval;
            refreshTimer = new DispatcherTimer();
            refreshTimer.Tick += new EventHandler(OnRefreshData);
            refreshTimer.Interval = TimeSpan.FromMilliseconds(interval);
            refreshTimer.Start();
            this.Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ((Storyboard)this.Resources["StoryboardFadeIn"]).Begin(this);
            LoadControls(GlobalClass._PermissionID);
            this.Cursor = Cursors.Arrow;
        }

        private void OnRefreshData(object sender, EventArgs e)
        {

            this.Cursor = Cursors.Wait;
            if (tablesStatus != null)
            {
                tablesStatus.GetAllTables();
            }
            if (cashierOrdersList != null)
            {
                cashierOrdersList.LoadOrders();
            }
            if (kitchenOrdersList != null)
            {
                kitchenOrdersList.LoadOrders();
            }

            this.Cursor = Cursors.Arrow;
        }

        private void LoadControls(int permissionID)
        {
            switch (permissionID)
            {
                //Manager
                case 1:
                    {
                        ManagementMenu.Visibility = Visibility.Visible;
                        UsersMenuItem.Visibility = Visibility.Visible;
                        UnitsMenuItem.Visibility = Visibility.Visible;
                        GroupsMenuItem.Visibility = Visibility.Visible;
                        ProductsMenuItem.Visibility = Visibility.Visible;
                        TablesMenuItem.Visibility = Visibility.Visible;
                        OrderMenu.Visibility = Visibility.Visible;
                        NewOrderMenuItem.Visibility = Visibility.Visible;
                        ToolsMenu.Visibility = Visibility.Visible;
                        SettingsMenuItem.Visibility = Visibility.Visible;
                        ChangePasswordMenuItem.Visibility = Visibility.Visible;
                        ReportsMenu.Visibility = Visibility.Visible;
                        SellReportMenuItem.Visibility = Visibility.Visible;
                        ProductPopularityReportMenuItem.Visibility = Visibility.Visible;

                        tablesStatus = new TablesStatusUserControl();
                        gridSmallArea.Children.Add(tablesStatus);
                        gridSmallArea.Children[0].SetValue(WidthProperty, gridSmallArea.Width);
                        gridSmallArea.Children[0].SetValue(HeightProperty, gridSmallArea.Height);

                        cashierOrdersList = new CashierOrdersListUserControl();
                        gridMainArea.Children.Add(cashierOrdersList);
                        gridMainArea.Children[0].SetValue(WidthProperty, gridMainArea.Width);
                        gridMainArea.Children[0].SetValue(HeightProperty, gridMainArea.Height);
                    }
                    break;
                //Cashier
                case 2:
                    {
                        ManagementMenu.Visibility = Visibility.Visible;
                        UsersMenuItem.Visibility = Visibility.Collapsed;
                        UnitsMenuItem.Visibility = Visibility.Visible;
                        GroupsMenuItem.Visibility = Visibility.Visible;
                        ProductsMenuItem.Visibility = Visibility.Visible;
                        TablesMenuItem.Visibility = Visibility.Visible;
                        OrderMenu.Visibility = Visibility.Visible;
                        NewOrderMenuItem.Visibility = Visibility.Visible;
                        ToolsMenu.Visibility = Visibility.Visible;
                        SettingsMenuItem.Visibility = Visibility.Collapsed;
                        ChangePasswordMenuItem.Visibility = Visibility.Visible;
                        ReportsMenu.Visibility = Visibility.Visible;
                        SellReportMenuItem.Visibility = Visibility.Visible;
                        ProductPopularityReportMenuItem.Visibility = Visibility.Visible;

                        tablesStatus = new TablesStatusUserControl();
                        gridSmallArea.Children.Add(tablesStatus);
                        gridSmallArea.Children[0].SetValue(WidthProperty, gridSmallArea.Width);
                        gridSmallArea.Children[0].SetValue(HeightProperty, gridSmallArea.Height);

                        cashierOrdersList = new CashierOrdersListUserControl();
                        gridMainArea.Children.Add(cashierOrdersList);
                        gridMainArea.Children[0].SetValue(WidthProperty, gridMainArea.Width);
                        gridMainArea.Children[0].SetValue(HeightProperty, gridMainArea.Height);

                    }
                    break;
                //Kitchen User
                case 3:
                    {
                        ManagementMenu.Visibility = Visibility.Collapsed;
                        UsersMenuItem.Visibility = Visibility.Collapsed;
                        UnitsMenuItem.Visibility = Visibility.Collapsed;
                        GroupsMenuItem.Visibility = Visibility.Collapsed;
                        ProductsMenuItem.Visibility = Visibility.Collapsed;
                        TablesMenuItem.Visibility = Visibility.Collapsed;
                        OrderMenu.Visibility = Visibility.Collapsed;
                        NewOrderMenuItem.Visibility = Visibility.Collapsed;
                        ToolsMenu.Visibility = Visibility.Visible;
                        SettingsMenuItem.Visibility = Visibility.Collapsed;
                        ChangePasswordMenuItem.Visibility = Visibility.Visible;
                        ReportsMenu.Visibility = Visibility.Collapsed;
                        SellReportMenuItem.Visibility = Visibility.Collapsed;
                        ProductPopularityReportMenuItem.Visibility = Visibility.Collapsed;

                        productAvailability = new ProductAvailabilityUserControl();
                        gridSmallArea.Children.Add(productAvailability);
                        gridSmallArea.Children[0].SetValue(WidthProperty, gridSmallArea.Width);
                        gridSmallArea.Children[0].SetValue(HeightProperty, gridSmallArea.Height);

                        kitchenOrdersList = new KitchenOrdersListUserControl();
                        gridMainArea.Children.Add(kitchenOrdersList);
                        gridMainArea.Children[0].SetValue(WidthProperty, gridSmallArea.Width);
                        gridMainArea.Children[0].SetValue(HeightProperty, gridSmallArea.Height);

                    }
                    break;
                default:
                    {
                        this.DialogResult = false;
                    }
                    break;
            }
        }

        private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                ((Storyboard)this.Resources["StoryboardFadeOut"]).Begin(this);
                UsersWindow objUsersWindow = new UsersWindow();
                objUsersWindow.Owner = this;
                objUsersWindow.ShowDialog();
                objUsersWindow = null;
                ((Storyboard)this.Resources["StoryboardFadeIn"]).Begin(this);
                this.Cursor = Cursors.Arrow;
            }
            catch
            {
                MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TablesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                ((Storyboard)this.Resources["StoryboardFadeOut"]).Begin(this);
                TablesWindow objTablesWindow = new TablesWindow();
                objTablesWindow.Owner = this;
                objTablesWindow.ShowDialog();
                objTablesWindow = null;
                ((Storyboard)this.Resources["StoryboardFadeIn"]).Begin(this);
                this.Cursor = Cursors.Arrow;
            }
            catch
            {
                MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProductsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedTab = "Products";
                switch (((MenuItem)e.Source).Name)
                {
                    case "UnitsMenuItem": selectedTab = "Units";
                        break;
                    case "GroupsMenuItem": selectedTab = "Groups";
                        break;
                    default: selectedTab = "Products";
                        break;
                }

                this.Cursor = Cursors.Wait;
                ((Storyboard)this.Resources["StoryboardFadeOut"]).Begin(this);
                ProductWindow objProductWindow = new ProductWindow(selectedTab);
                objProductWindow.Owner = this;
                objProductWindow.ShowDialog();
                objProductWindow = null;
                ((Storyboard)this.Resources["StoryboardFadeIn"]).Begin(this);
                this.Cursor = Cursors.Arrow;
            }
            catch
            {
                MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NewOrderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard)this.Resources["StoryboardFadeOut"]).Begin(this);
            NewOrderWindow objNewOrderWindow = new NewOrderWindow();
            objNewOrderWindow.Owner = this;
            bool? dg = objNewOrderWindow.ShowDialog();
            if (dg == true)
            {
                if (cashierOrdersList != null)
                {
                    cashierOrdersList.LoadOrders();
                }
                if (kitchenOrdersList != null)
                {
                    kitchenOrdersList.LoadOrders();
                }
            }
            ((Storyboard)this.Resources["StoryboardFadeIn"]).Begin(this);
        }

        private void ChangePasswordMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                ((Storyboard)this.Resources["StoryboardFadeOut"]).Begin(this);
                ChangePasswordWindow objChangePasswordWindow = new ChangePasswordWindow();
                objChangePasswordWindow.Owner = this;
                objChangePasswordWindow.ShowDialog();
                objChangePasswordWindow = null;
                ((Storyboard)this.Resources["StoryboardFadeIn"]).Begin(this);
                this.Cursor = Cursors.Arrow;
            }
            catch
            {
                MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard)this.Resources["StoryboardFadeOut"]).Begin(this);
            SettingsWindow objSettingsWindow = new SettingsWindow();
            objSettingsWindow.Owner = this;
            objSettingsWindow.ShowDialog();
            objSettingsWindow = null;
            ((Storyboard)this.Resources["StoryboardFadeIn"]).Begin(this);
        }

        private void Dan_Clicked(object sender, MouseButtonEventArgs e)
        {
            Process.Start("www.daneshmandi.com");
        }

        private void SellReportMenuItem_Click(object sender, RoutedEventArgs e)
        {

            ((Storyboard)this.Resources["StoryboardFadeOut"]).Begin(this);
            SellReportWindow objSellReportWindow = new SellReportWindow();
            objSellReportWindow.Owner = this;
            objSellReportWindow.ShowDialog();
            objSellReportWindow = null;
            ((Storyboard)this.Resources["StoryboardFadeIn"]).Begin(this);
        }

        private void ProductPopularityReportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard)this.Resources["StoryboardFadeOut"]).Begin(this);
            ProductPopularityReportWindow objProductPopularityReportWindow = new ProductPopularityReportWindow();
            objProductPopularityReportWindow.Owner = this;
            objProductPopularityReportWindow.ShowDialog();
            objProductPopularityReportWindow = null;
            ((Storyboard)this.Resources["StoryboardFadeIn"]).Begin(this);
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard)this.Resources["StoryboardFadeOut"]).Begin(this);
            AboutWindow objAboutWindow = new AboutWindow();
            objAboutWindow.Owner = this;
            objAboutWindow.ShowDialog();
            objAboutWindow = null;
            ((Storyboard)this.Resources["StoryboardFadeIn"]).Begin(this);
        }

    }
}
