using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SWOMT.Converter
{
   public class MyBoolConverterPresentAbsent :IValueConverter 
    {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			switch (value.ToString().ToLower())
			{
				case "true":
					//case "oui":
					return "Présent";
				//case "no":
				case "false":
					return "Absent";
			}
			return "Absent";
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is bool)
			{
				if ((bool)value == true)
					return "Présent";
				else
					return "Absent";
			}
			return "Absent";
		}
	}
}
