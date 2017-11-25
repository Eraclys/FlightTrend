using FlightTrend.Core.Serialization;
using FlightTrend.Core.Validation;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Serializers
{
    [UsedImplicitly]
    public sealed class LocalDateSerializer : ISerializer<LocalDate>
    {
        [NotNull]
        public string Serialize(LocalDate value)
        {
            return $"{value.Year}-{value.Month}-{value.Day}";
        }

        public LocalDate Deserialize(string value)
        {
            Guard.MustNotBeNullOrWhiteSpace(value, nameof(value));

            var values = value.Split('-');
            var year = int.Parse(values[0]);
            var month = int.Parse(values[1]);
            var day = int.Parse(values[2]);

            return new LocalDate(year, month, day);
        }
    }
}