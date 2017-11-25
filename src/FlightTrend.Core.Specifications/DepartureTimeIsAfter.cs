using NodaTime;

namespace FlightTrend.Core.Specifications
{
    public sealed class DepartureTimeIsAfter : ISpecification<FlightPrice>
    {
        private readonly LocalTime _localTime;

        public DepartureTimeIsAfter(LocalTime localTime)
        {
            _localTime = localTime;
        }

        public bool IsSatisfiedBy(FlightPrice value)
        {
            return value.DepartureTime > _localTime;
        }
    }
}