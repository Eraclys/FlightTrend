using FlightTrend.Core.Models;
using NodaTime;

namespace FlightTrend.Core.Specifications
{
    public sealed class DepartureTimeIsAfter : ISpecification<Flight>
    {
        private readonly LocalTime _localTime;

        public DepartureTimeIsAfter(LocalTime localTime)
        {
            _localTime = localTime;
        }

        public bool IsSatisfiedBy(Flight value)
        {
            return value.DepartureTime > _localTime;
        }
    }
}