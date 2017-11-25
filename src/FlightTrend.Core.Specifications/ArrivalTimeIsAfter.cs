using NodaTime;

namespace FlightTrend.Core.Specifications
{
    public sealed class ArrivalTimeIsAfter : ISpecification<FlightPrice>
    {
        private readonly LocalTime _localTime;

        public ArrivalTimeIsAfter(LocalTime localTime)
        {
            _localTime = localTime;
        }

        public bool IsSatisfiedBy(FlightPrice value)
        {
            return value.DepartureTime > _localTime;
        }
    }
}