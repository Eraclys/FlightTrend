using FlightTrend.Core.Serialization;
using NodaTime;

namespace FlightTrend.Serializers
{
    public class LocalTimeSerializer : ISerializer<LocalTime>
    {
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