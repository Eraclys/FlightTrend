using System.Globalization;
using FlightTrend.Core.Serialization;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Serializers
{
    [UsedImplicitly]
    public sealed class InstantSerializer : ISerializer<Instant>
    {
        public string Serialize(Instant value)
        {
            return value.ToUnixTimeTicks().ToString(CultureInfo.InvariantCulture);
        }

        public Instant Deserialize(string value)
        {
            return Instant.FromUnixTimeTicks(long.Parse(value));
        }
    }
}