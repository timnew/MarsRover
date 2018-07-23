using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ThoughtWorks.CodingTests.MarsRovers.ValueConverters
{
    public class VisualOrientationConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var logicOrientation = (Orientation)value;

            double result = 0; //Initial Value is Compensation
            switch (logicOrientation)
            {
                case Orientation.North:
                    result = 0;
                    break;
                case Orientation.West:
                    result = 90;
                    break;
                case Orientation.South:
                    result = 180;
                    break;
                case Orientation.East:
                    result = 270;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
