using NodaTime;

namespace FlightTrend.Core.Specifications
{
    public static class FlightPriceSpecifications
    {
        public static ISpecification<FlightPrice> ArrivalTimeIsBefore(LocalTime localTime)
        {
            return new ArrivalTimeIsBefore(localTime);
        }

        public static ISpecification<FlightPrice> DepartureTimeIsBefore(LocalTime localTime)
        {
            return new DepartureTimeIsBefore(localTime);
        }

        public static ISpecification<FlightPrice> ArrivalTimeIsAfter(LocalTime localTime)
        {
            return new ArrivalTimeIsAfter(localTime);
        }

        public static ISpecification<FlightPrice> DepartureTimeIsAfter(LocalTime localTime)
        {
            return new DepartureTimeIsAfter(localTime);
        }
    }
}