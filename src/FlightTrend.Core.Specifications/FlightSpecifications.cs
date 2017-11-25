using FlightTrend.Core.Models;
using NodaTime;

namespace FlightTrend.Core.Specifications
{
    public static class FlightSpecifications
    {
        public static ISpecification<Flight> ArrivalTimeIsBefore(LocalTime localTime)
        {
            return new ArrivalTimeIsBefore(localTime);
        }

        public static ISpecification<Flight> DepartureTimeIsBefore(LocalTime localTime)
        {
            return new DepartureTimeIsBefore(localTime);
        }

        public static ISpecification<Flight> ArrivalTimeIsAfter(LocalTime localTime)
        {
            return new ArrivalTimeIsAfter(localTime);
        }

        public static ISpecification<Flight> DepartureTimeIsAfter(LocalTime localTime)
        {
            return new DepartureTimeIsAfter(localTime);
        }
    }
}