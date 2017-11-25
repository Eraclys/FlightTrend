using FlightTrend.Core.Serialization;
using System.Globalization;

namespace FlightTrend.Serializers
{
    public sealed class DecimalSerializer : ISerializer<decimal>
    {
        public string Serialize(decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public decimal Deserialize(string value)
        {
            return decimal.Parse(value);
        }
    }
}