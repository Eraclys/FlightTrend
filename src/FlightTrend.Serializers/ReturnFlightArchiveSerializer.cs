using FlightTrend.Core.Models;
using FlightTrend.Core.Serialization;
using NodaTime;
using System.Collections.Generic;
using System.Linq;

namespace FlightTrend.Serializers
{
    public class ReturnFlightArchiveSerializer : ISerializer<ReturnFlightArchive>
    {
        private const string Separator = "|";
        private readonly ISerializer<LocalDate> _dateSerializer;
        private readonly ISerializer<decimal> _decimalSerializer;
        private readonly ISerializer<Instant> _instantSerializer;
        private readonly ISerializer<LocalTime> _timeSerializer;

        public ReturnFlightArchiveSerializer(
            ISerializer<Instant> instantSerializer,
            ISerializer<LocalDate> dateSerializer,
            ISerializer<LocalTime> timeSerializer,
            ISerializer<decimal> decimalSerializer)
        {
            _instantSerializer = instantSerializer;
            _dateSerializer = dateSerializer;
            _timeSerializer = timeSerializer;
            _decimalSerializer = decimalSerializer;
        }

        public string Serialize(ReturnFlightArchive value)
        {
            if (value == null)
                return null;

            var values = new[] {_instantSerializer.Serialize(value.Instant)}
                .Concat(SerializeFlight(value.ReturnFlight.Departure))
                .Concat(SerializeFlight(value.ReturnFlight.Return));

            return string.Join(Separator, values);
        }

        public ReturnFlightArchive Deserialize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            var values = value.Split(Separator.ToCharArray());

            return new ReturnFlightArchive(
                _instantSerializer.Deserialize(values[0]),
                new ReturnFlight(
                    DeserializeFlight(values.Skip(1).Take(8).ToArray()),
                    DeserializeFlight(values.Skip(9).Take(8).ToArray())));
        }

        private Flight DeserializeFlight(string[] values)
        {
            return new Flight(
                values[0],
                values[1],
                values[2],
                _dateSerializer.Deserialize(values[3]),
                _timeSerializer.Deserialize(values[4]),
                _dateSerializer.Deserialize(values[5]),
                _timeSerializer.Deserialize(values[6]),
                _decimalSerializer.Deserialize(values[7]));
        }

        private IEnumerable<string> SerializeFlight(Flight flight)
        {
            return new[]
            {
                flight.Company,
                flight.From,
                flight.To,
                _dateSerializer.Serialize(flight.DepartureDate),
                _timeSerializer.Serialize(flight.DepartureTime),
                _dateSerializer.Serialize(flight.ArrivalDate),
                _timeSerializer.Serialize(flight.ArrivalTime),
                _decimalSerializer.Serialize(flight.Price)
            };
        }
    }
}