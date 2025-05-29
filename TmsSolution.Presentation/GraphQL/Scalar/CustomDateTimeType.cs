using HotChocolate.Language;
using System.Globalization;

namespace TmsSolution.Presentation.GraphQL.Scalar
{
    public class CustomDateTimeType : ScalarType<DateTime, StringValueNode>
    {
        private const string FORMAT = "yyyy-MM-dd HH:mm:ss";

        public CustomDateTimeType() : base("CustomDateTime") { }

        protected override DateTime ParseLiteral(StringValueNode valueSyntax)
        {
            if (DateTime.TryParseExact(valueSyntax.Value, FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
            {
                return dateTime;
            }
            throw new SerializationException($"Invalid date format. Expected format: {FORMAT}", this);
        }

        protected override StringValueNode ParseValue(DateTime runtimeValue)
        {
            return new StringValueNode(runtimeValue.ToString(FORMAT, CultureInfo.InvariantCulture));
        }

        public override IValueNode ParseResult(object resultValue)
        {
            if (resultValue is DateTime dateTime)
            {
                return ParseValue(dateTime);
            }
            throw new SerializationException($"The specified value is not a valid {Name} value.", this);
        }

        public override bool TrySerialize(object runtimeValue, out object resultValue)
        {
            if (runtimeValue is DateTime dateTime)
            {
                resultValue = dateTime.ToString(FORMAT, CultureInfo.InvariantCulture);
                return true;
            }
            resultValue = null;
            return false;
        }

        public override bool TryDeserialize(object resultValue, out object runtimeValue)
        {
            if (resultValue is string str &&
                DateTime.TryParseExact(str, FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
            {
                runtimeValue = dateTime;
                return true;
            }
            runtimeValue = null;
            return false;
        }
    }
}
