using FlightTrend.Core.Serialization;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;

namespace FlightTrend.Serializers.Test
{
    [TestClass]
    public sealed class LocalDateSerializerTest
    {
        private readonly ISerializer<LocalDate> _sut = new LocalDateSerializer();

        [TestMethod]
        public void SerializeAndDeserializeShouldReturnTheSame()
        {
            var expected = Generators.RandomDate();

            var serialized = _sut.Serialize(expected);

            var deserialized = _sut.Deserialize(serialized);

            deserialized.ShouldBeEquivalentTo(expected);
        }
    }
}