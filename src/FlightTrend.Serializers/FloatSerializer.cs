using FlightTrend.Core.Serialization;
using System.Globalization;
using JetBrains.Annotations;

namespace FlightTrend.Serializers
{
    public sealed class FloatSerializer : ISerializer<float>
    {
        [NotNull]
        public string Serialize(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public float Deserialize(string value)
        {
            return float.Parse(value);
        }
    }
}