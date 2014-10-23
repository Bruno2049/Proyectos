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
using System.Windows.Media.Animation;

namespace Restaurant
{
	/// <summary>
	/// Interaction logic for ProductWindow.xaml
	/// </summary>
	public partial class ProductWindow : Window
	{
		public ProductWindow(string selectedTab)
		{
			this.InitializeComponent();
            // Insert code required on object creation below this point.

            switch (selectedTab)
            {
                case "Units":
                    {
                        ChangeSelectedTab("Units");
                        ((Storyboard)this.Resources["StoryboardUnitFadeIn"]).Begin(this);
                    }
                    break;
                case "Groups":
                    {
                        ChangeSelectedTab("Groups");
                        ((Storyboard)this.Resources["StoryboardGroupFadeIn"]).Begin(this);
                    }
                    break;
                case "Products":
                    {
                        ChangeSelectedTab("Products");
                        ((Storyboard)this.Resources["StoryboardProductFadeIn"]).Begin(this);
                    }
                    break;
                default:
                    {
                        ChangeSelectedTab("ProductsTree");
                        ((Storyboard)this.Resources["StoryboardProductTreeFadeIn"]).Begin(this);
                    }
                    break;
            }
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

       
        private void TabButtonUnit_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedTab("Units");
        }

        private void TabButtonGroup_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ChangeSelectedTab("Groups");
            this.Cursor = Cursors.Arrow;
        }

        private void TabButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ChangeSelectedTab("Products");
            ProductsPane.Refresh();
            this.Cursor = Cursors.Arrow;
        }

        private void TabButtonProductTree_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            ProductsTreePane.RefreshProductsList();
            ChangeSelectedTab("ProductsTree");
            this.Cursor = Cursors.Arrow;
        }

        private void ChangeSelectedTab(string selectedTab)
        {
            switch (selectedTab)
            {
                case "Units":
                    {
                        TabButtonUnit.IsEnabled = false;
                        TabButtonGroup.IsEnabled = true;
                        TabButtonProduct.IsEnabled = true;
                        TabButtonProductTree.IsEnabled = true;
                    }
                    break;
                case "Groups":
                    {
                        TabButtonGroup.IsEnabled = false;
                        TabButtonUnit.IsEnabled = true;
                        TabButtonProduct.IsEnabled = true;
                        TabButtonProductTree.IsEnabled = true;
                    }
                    break;
                case "Products":
                    {
                        TabButtonProduct.IsEnabled = false;
                        TabButtonUnit.IsEnabled = true;
                        TabButtonGroup.IsEnabled = true;
                        TabButtonProductTree.IsEnabled = true;
                    }
                    break;
                default:
                    {
                        TabButtonProductTree.IsEnabled = false;
                        TabButtonUnit.IsEnabled = true;
                        TabButtonGroup.IsEnabled = true;
                        TabButtonProduct.IsEnabled = true;
                    }
                    break;
            }
        }
	}
}