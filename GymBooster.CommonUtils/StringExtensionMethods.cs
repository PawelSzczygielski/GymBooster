using System;
using System.Globalization;
using System.Linq;

namespace GymBooster.CommonUtils
{
    public static class StringExtensionMethods
    {
        public static DateTime? ParseDateTime(this string dateToParse, string[] formats = null,
            IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.None)
        {
            var CustomDateFormats = new string[]
                {
                "yyyyMMddTHHmmssZ",
                "yyyyMMddTHHmmZ",
                "yyyyMMddTHHmmss",
                "yyyyMMddTHHmm",
                "yyyyMMddHHmmss",
                "yyyyMMddHHmm",
                "yyyyMMdd",
                "yyyy-MM-ddTHH-mm-ss",
                "yyyy-MM-dd-HH-mm-ss",
                "yyyy-MM-dd-HH-mm",
                "yyyy-MM-dd",
                "MM-dd-yyyy"
                };

            if (formats == null || !formats.Any())
            {
                formats = CustomDateFormats;
            }

            DateTime validDate;

            foreach (var format in formats)
            {
                if (format.EndsWith("Z"))
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                             provider,
                             DateTimeStyles.AssumeUniversal,
                             out validDate))
                    {
                        return validDate;
                    }
                }

                if (DateTime.TryParseExact(dateToParse, format,
                         provider, styles, out validDate))
                {
                    return validDate;
                }
            }

            return null;
        }        
    }
}
