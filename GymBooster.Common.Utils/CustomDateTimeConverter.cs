using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymBooster.Common.Utils
{
    public class CustomDateTimeConverter : DateTimeConverterBase
    {
        private readonly string _dateFormat;
        private readonly DateTimeConverterBase _innerConverter;

        public CustomDateTimeConverter() : this(null)
        {
        }

        public CustomDateTimeConverter(string dateFormat = null) : this(dateFormat, new IsoDateTimeConverter())
        {
        }

        public CustomDateTimeConverter(string dateFormat = null, DateTimeConverterBase innerConverter = null)
        {
            _dateFormat = dateFormat;
            _innerConverter = innerConverter ?? new IsoDateTimeConverter();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var isNullableType = objectType.IsNullableType();

            if (reader.TokenType == JsonToken.Null)
            {
                if (isNullableType)
                    return null;

                throw new JsonSerializationException($"Cannot convert null value to {objectType}");
            }

            if (reader.TokenType == JsonToken.Date)
                return reader.Value;

            if (reader.TokenType != JsonToken.String)
                throw new JsonSerializationException($"Unexpected token parsing date. Expected {nameof(String)}, got {reader.TokenType}.");

            var dateToParse = reader.Value.ToString();

            if (isNullableType && string.IsNullOrWhiteSpace(dateToParse))
            {
                return null;
            }

            if (string.IsNullOrEmpty(_dateFormat))
            {
                return dateToParse.ParseDateTime();
            }

            return dateToParse.ParseDateTime(new[] { _dateFormat });
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            _innerConverter?.WriteJson(writer, value, serializer);
        }
    }
}
