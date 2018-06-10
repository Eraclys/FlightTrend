using FlightTrend.Core.Serialization;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;

namespace FlightTrend.Serializers.Test
{
    [TestClass]
    public sealed class InstantSerializerTest
    {

        private readonly ISerializer<Instant> _sut = new InstantSerializer();

        [TestMethod]
        public void SerializeAndDeserializeShouldReturnTheSame()
        {
            var expected = SystemClock.Instance.GetCurrentInstant();

            var serialized = _sut.Serialize(expected);

            var deserialized = _sut.Deserialize(serialized);

            deserialized.Should().BeEquivalentTo(expected);
        }
    }
}