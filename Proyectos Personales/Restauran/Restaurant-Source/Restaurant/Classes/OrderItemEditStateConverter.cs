using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;

namespace Restaurant.Classes
{
    class OrderItemEditStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            Byte state = (Byte)value;
            if (state == 0)
            {
                return "/Images/New.ico";
            }
            else if (state == 1 || state == 2)
            {
                return "/Images/edit.ico";
            }
            else if (state == 3)
            {
                return "/Images/deleted.ico";
            }
            else
            {
                return "/Images/Unknown.ico";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            throw new NotSupportedException("This converter is for grouping only.");
        }
    }
}
