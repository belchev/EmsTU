using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Common.Utils
{
    public static class Formatter
    {
        public const string DecimalFormat = "0.00";
        public const string DateTimeFormat = "dd.MM.yyyy";
        public const string DateTimeHHMMFormat = "dd.MM.yyyy HH:mm";
        public const string DateTimeFormatImport = "yyyyMMdd";
        public const char Int32PaddingChar = '0';
        public const string TimeSpanFormat = @"hh\:mm\:ss";

        public const string SqlServerDateTimeFormat = "yyyy-MM-dd HH:mm";

        public static bool TryParseDateTime(string s, out DateTime result)
        {
            return DateTime.TryParseExact(s, DateTimeFormatImport, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        public static bool TryParseDatetime(string s, string format, out DateTime result)
        {
            return DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        public static bool TryParseDecimal(string s, out decimal result)
        {
            result = 0;

            if (!Decimal.TryParse(s, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                new NumberFormatInfo { NumberDecimalSeparator = "." }, out result))
            {
                return Decimal.TryParse(s, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                    new NumberFormatInfo { NumberDecimalSeparator = "," }, out result);
            }

            return true;
        }

        public static string GetDomain(Uri uri)
        {
            if (uri != null)
            {
                return uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
            }

            return string.Empty;
        }

        public static string LimitLength(this string s, int length)
        {
            return s.Length > length ? s.Substring(0, length) : s;
        }

        public static string FromMoney(decimal? money)
        {
            return money == null ? null : money.Value.ToString("N2");
        }

        public static decimal? ParseToDecimal(object value, int? precision)
        {
            if (value == null)
                return null;

            string separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string val = value.ToString().Replace(".", separator);
            val = val.Replace(",", separator);
            val = val.Replace(" ", "");
            val = val.Replace("%", "");

            decimal result;
            if (decimal.TryParse(val, out result))
            {
                if (precision.HasValue)
                    return decimal.Round(result, precision.Value, MidpointRounding.AwayFromZero);
                else
                    return result;
            }
            else
                return null;
        }

        public static decimal? ToMoney(object value)
        {
            return ParseToDecimal(value, 2);
        }

        public static string RemoveDomain(string username)
        {
            return username.Substring(username.IndexOf('\\')); //keeps '\' before username
        }
    }
}
