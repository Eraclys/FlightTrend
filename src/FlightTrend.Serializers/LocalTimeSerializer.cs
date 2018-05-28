using FlightTrend.Core.Serialization;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Serializers
{
    [UsedImplicitly]
    public sealed class LocalTimeSerializer : ISerializer<LocalTime>
    {
        [NotNull]
        public string Serialize(LocalTime value)
        {
            return value.TickOfDay.ToString();
        }

        public LocalTime Deserialize(string value)
        {
            return LocalTime.FromTicksSinceMidnight(long.Parse(value));
        }
    }
}