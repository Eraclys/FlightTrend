using FlightTrend.Core.Models;
using FlightTrend.Core.Serialization;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlightTrend.Serializers.Test
{
    [TestClass]
    public sealed class ReturnFlightArchiveSerializerTest
    {
        private readonly ISerializer<ReturnFlightArchive> _sut =
            new ReturnFlightArchiveSerializer(
                new InstantSerializer(),
                new LocalDateSerializer(),
                new LocalTimeSerializer(),
                new DecimalSerializer());

        [TestMethod]
        public void SerializeAndDeserializeShouldReturnTheSame()
        {
            var expected = Generators.GenerateReturnFlightArchive();

            var serialized = _sut.Serialize(expected);

            var deserialized = _sut.Deserialize(serialized);

            deserialized.ShouldBeEquivalentTo(expected);
        }
    }
}