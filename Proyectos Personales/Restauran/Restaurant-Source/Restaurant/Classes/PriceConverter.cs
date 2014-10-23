using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;

namespace Restaurant.Classes
{
    class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
       CultureInfo culture)
        {
            string temp = value.ToString();
            if (temp.EndsWith(".00"))
            {
                temp=temp.Split('.')[0];
            }
            return temp + " $";
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            throw new NotSupportedException("This converter is for grouping only.");
        }
    }
}
