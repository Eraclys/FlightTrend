using NodaTime;

namespace FlightTrend.Core.Specifications
{
    public sealed class ArrivalTimeIsBefore : ISpecification<FlightPrice>
    {
        private readonly LocalTime _localTime;

        public ArrivalTimeIsBefore(LocalTime localTime)
        {
            _localTime = localTime;
        }

        public bool IsSatisfiedBy(FlightPrice value)
        {
            return value.DepartureTime < _localTime;
        }
    }
}