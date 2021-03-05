using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SWOMT.Converter
{
   public class MyBoolConverterReussiEchoue :IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			switch (value.ToString().ToLower())
			{
				case "true":
					//case "oui":
					return "Réussi";
				//case "no":
				case "false":
					return "Echoué";
			}
			return "Echoué";
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is bool)
			{
				if ((bool)value == true)
					return "Réussi";
				else
					return "Echoué";
			}
			return "Echoué";
		}
	}
}
