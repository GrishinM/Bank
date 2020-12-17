using System;
using System.Globalization;
using System.Windows.Data;
using BankLibrary.Requests;

namespace ATM.Converters
{
    public class AuthorizationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var cardId = (int) values[0];
            int.TryParse((string) values[1], out var password);
            return new Authorization {CardId = cardId, Password = password};
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}