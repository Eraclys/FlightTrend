using FlightTrend.Core.Models;
using FlightTrend.Core.Serialization;
using JetBrains.Annotations;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightTrend.Serializers
{
    [UsedImplicitly]
    public sealed class ReturnFlightArchiveSerializer : ISerializer<ReturnFlightArchive>
    {
        private const string Separator = "|";
        private readonly ISerializer<LocalDate> _dateSerializer;
        private readonly ISerializer<float> _floatSerializer;
        private readonly ISerializer<Instant> _instantSerializer;
        private readonly ISerializer<LocalTime> _timeSerializer;

        public ReturnFlightArchiveSerializer(
            ISerializer<Instant> instantSerializer,
            ISerializer<LocalDate> dateSerializer,
            ISerializer<LocalTime> timeSerializer,
            ISerializer<float> floatSerializer)
        {
            _instantSerializer = instantSerializer;
            _dateSerializer = dateSerializer;
            _timeSerializer = timeSerializer;
            _floatSerializer = floatSerializer;
        }

        public string Serialize(ReturnFlightArchive value)
        {
            if (value == null)
            {
                return null;
            }

            var values = new[] {_instantSerializer.Serialize(value.Instant)}
                .Concat(SerializeFlight(value.ReturnFlight.Departure))
                .Concat(SerializeFlight(value.ReturnFlight.Return));

            return string.Join(Separator, values);
        }

        public ReturnFlightArchive Deserialize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var values = value.Split(new[] {Separator}, StringSplitOptions.RemoveEmptyEntries);

            return new ReturnFlightArchive(
                _instantSerializer.Deserialize(values[0]),
                new ReturnFlight(
                    DeserializeFlight(values.Skip(1).Take(9).ToArray()),
                    DeserializeFlight(values.Skip(10).Take(9).ToArray())));
        }

        [NotNull]
        private Flight DeserializeFlight([NotNull] IReadOnlyList<string> values)
        {
            return new Flight(
                values[0],
                values[1],
                values[2],
                _dateSerializer.Deserialize(values[3]),
                _dateSerializer.Deserialize(values[4]),
                _timeSerializer.Deserialize(values[5]),
                _dateSerializer.Deserialize(values[6]),
                _timeSerializer.Deserialize(values[7]),
                _floatSerializer.Deserialize(values[8]));
        }

        [NotNull]
        private IEnumerable<string> SerializeFlight([NotNull] Flight flight)
        {
            return new[]
            {
                flight.Company,
                flight.From,
                flight.To,
                _dateSerializer.Serialize(flight.RequestedDate),
                _dateSerializer.Serialize(flight.DepartureDate),
                _timeSerializer.Serialize(flight.DepartureTime),
                _dateSerializer.Serialize(flight.ArrivalDate),
                _timeSerializer.Serialize(flight.ArrivalTime),
                _floatSerializer.Serialize(flight.Price)
            };
        }
    }
}