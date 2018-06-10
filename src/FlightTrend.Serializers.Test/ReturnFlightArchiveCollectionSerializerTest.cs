using FlightTrend.Core.Models;
using FlightTrend.Core.Serialization;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FlightTrend.Serializers.Test
{
    [TestClass]
    public sealed class ReturnFlightArchiveCollectionSerializerTest
    {
        private readonly ISerializer<IEnumerable<ReturnFlightArchive>> _sut =
            new ReturnFlightArchiveCollectionSerializer(
                new ReturnFlightArchiveSerializer(
                    new InstantSerializer(),
                    new LocalDateSerializer(),
                    new LocalTimeSerializer(),
                    new FloatSerializer()));

        [TestMethod]
        public void SerializeAndDeserializeShouldReturnTheSame()
        {
            var expected = Generators.GenerateListOfReturnFlightArchive().ToList();

            var serialized = _sut.Serialize(expected);

            var deserialized = _sut.Deserialize(serialized);

            deserialized.Should().AllBeEquivalentTo(expected);
        }
    }
}
