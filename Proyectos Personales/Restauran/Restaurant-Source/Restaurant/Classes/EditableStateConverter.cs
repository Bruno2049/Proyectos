using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
namespace Restaurant.Classes
{
    class EditableStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
         CultureInfo culture)
        {
            Byte state = (Byte)value;
            if (state == 0)
            {
                return Visibility.Visible;
            }
            else if (state == 1)
            {
                return Visibility.Visible;
            }
            else if (state == 2)
            {
                return Visibility.Collapsed;
            }
            else if (state == 3)
            {
                return Visibility.Collapsed;
            }
            else if (state == 4)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            throw new NotSupportedException("This converter is for grouping only.");
        }
    }
}
