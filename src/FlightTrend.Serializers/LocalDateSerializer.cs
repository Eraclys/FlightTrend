using FlightTrend.Core.Serialization;
using NodaTime;
using System.Globalization;

namespace FlightTrend.Serializers
{
    public class LocalDateSerializer : ISerializer<LocalDate>
    {
        public string Serialize(LocalDate value)
        {
            return $"{value.Year}-{value.Month}-{value.Day}";
        }

        public LocalDate Deserialize(string value)
        {
            var values = value.Split('-');
            var year = int.Parse(values[0]);
            var month = int.Parse(values[0]);
            var day = int.Parse(values[0]);

            return new LocalDate(year, month, day);
        }
    }

    public class InstantSerializer : ISerializer<Instant>
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