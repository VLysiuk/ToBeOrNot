using System;
using System.Globalization;
using System.Windows.Data;

namespace ToBeOrNot.Misc
{
    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse(value.GetType(), parameter.ToString(), true).Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? parameter : null;
        }
    }
}
