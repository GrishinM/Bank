using System;
using System.Globalization;
using System.Windows.Data;
using BankLibrary.Requests;

namespace ATM.Converters
{
    public class TransferConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            long.TryParse((string) values[0], out var cardToNumber);
            double.TryParse((string) values[1], out var amount);
            return new Transfer {CardToNumber = cardToNumber, Amount = amount};
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}