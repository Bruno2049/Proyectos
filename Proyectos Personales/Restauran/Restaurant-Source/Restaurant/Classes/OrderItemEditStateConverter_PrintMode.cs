using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Restaurant.Classes
{
    class OrderItemEditStateConverter_PrintMode : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {

            byte state = (byte)value;
            if (state == 3)
            {
                return TextDecorations.Strikethrough;
            }
            else
            {
                return null;
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            throw new NotSupportedException("This converter is for grouping only.");
        }
    }
}
