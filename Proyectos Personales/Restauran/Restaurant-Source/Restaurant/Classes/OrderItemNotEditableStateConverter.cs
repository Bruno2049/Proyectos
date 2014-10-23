using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;

namespace Restaurant.Classes
{
    class OrderItemNotEditableStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            bool notEditableState = (bool)value;
            if (notEditableState == true)
            {
                return "/Images/UnEditable.ico";
            }
            else if (notEditableState == false)
            {
                return "/Images/edit.ico";
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
