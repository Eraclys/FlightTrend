using FlightTrend.Core.Serialization;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;

namespace FlightTrend.Serializers.Test
{
    [TestClass]
    public sealed class LocalTimeSerializerTest
    {
        private readonly ISerializer<LocalTime> _sut = new LocalTimeSerializer();

        [TestMethod]
        public void SerializeAndDeserializeShouldReturnTheSame()
        {
            var expected = Generators.RandomTime();

            var serialized = _sut.Serialize(expected);

            var deserialized = _sut.Deserialize(serialized);

            deserialized.ShouldBeEquivalentTo(expected);
        }
    }
}