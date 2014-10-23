using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;

namespace Restaurant.Classes
{
    class TablesStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            try
            {
                bool state = (bool)value;
                if (state == true)
                {
                    return "/Images/BusyTable.ico";
                }
                else
                {
                    return "/Images/FreeTable.ico";
                }
            }
            catch 
            {
                string state = (string)value;
                if (state == "True")
                {
                    return "/Images/BusyTable.ico";
                }
                else
                {
                    return "/Images/FreeTable.ico";
                }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            throw new NotSupportedException("This converter is for grouping only.");
        }
    }
}
