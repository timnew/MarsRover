using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using ThoughtWorks.CodingTests.MarsRovers.ViewModels;

namespace ThoughtWorks.CodingTests.MarsRovers.ValueConverters
{
    public class VisualCoordinateConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var logicalCoord = (int)value;
            var rate = ViewModelLocator.MarsRoversStatic.PlateauCellVisualSize;

            return rate * logicalCoord;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
