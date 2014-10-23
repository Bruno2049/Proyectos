using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restaurant
{
    public class SortListViewColumn : GridViewColumn
    {

        public string SortProperty
        {
            get { return (string)GetValue(SortPropertyProperty); }
            set { SetValue(SortPropertyProperty, value); }
        }

        public static readonly DependencyProperty SortPropertyProperty =
            DependencyProperty.Register("SortProperty",
            typeof(string), typeof(SortListViewColumn));

        public string SortStyle
        {
            get { return (string)GetValue(SortStyleProperty); }
            set { SetValue(SortStyleProperty, value); }
        }

        public static readonly DependencyProperty SortStyleProperty =
            DependencyProperty.Register("SortStyle",
            typeof(string), typeof(SortListViewColumn));

    }
}
