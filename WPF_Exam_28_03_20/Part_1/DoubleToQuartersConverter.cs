using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace WPF_Exam_28_03_20
{
    class DoubleToQuartersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {           
            string toReturn = string.Empty;
            double minVal = ((double[])parameter)[0];
            double maxVal = ((double[])parameter)[1];

            double inputVal = double.Parse(value.ToString()) - minVal;
            double parameterVal = maxVal - minVal;

            


            if (inputVal <= parameterVal / 4) toReturn = "SMALL";
            if (inputVal > parameterVal / 4 && inputVal <= parameterVal / 2) toReturn = "MEDIUM";
            if (inputVal > parameterVal / 2 && inputVal <= (parameterVal / 4)*3) toReturn = "LARGE";
            if (inputVal > (parameterVal / 4) * 3) toReturn = "EXTRA LARGE";

            return toReturn;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
