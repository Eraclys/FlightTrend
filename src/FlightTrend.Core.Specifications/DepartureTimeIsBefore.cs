using NodaTime;

namespace FlightTrend.Core.Specifications
{
    public sealed class DepartureTimeIsBefore : ISpecification<FlightPrice>
    {
        private readonly LocalTime _localTime;

        public DepartureTimeIsBefore(LocalTime localTime)
        {
            _localTime = localTime;
        }

        public bool IsSatisfiedBy(FlightPrice value)
        {
            return value.DepartureTime < _localTime;
        }
    }
}
