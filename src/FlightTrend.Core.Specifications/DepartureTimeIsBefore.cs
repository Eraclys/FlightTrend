using FlightTrend.Core.Models;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Core.Specifications
{
    [UsedImplicitly]
    public sealed class DepartureTimeIsBefore : ISpecification<Flight>
    {
        private readonly LocalTime _localTime;

        public DepartureTimeIsBefore(LocalTime localTime)
        {
            _localTime = localTime;
        }

        public bool IsSatisfiedBy(Flight value)
        {
            return value.DepartureTime < _localTime;
        }
    }
}
