using FlightTrend.Core.Models;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Core.Specifications
{
    [UsedImplicitly]
    public static class FlightSpecifications
    {
        [NotNull]
        public static ISpecification<Flight> ArrivalTimeIsBefore(LocalTime localTime)
        {
            return new ArrivalTimeIsBefore(localTime);
        }

        [NotNull]
        public static ISpecification<Flight> DepartureTimeIsBefore(LocalTime localTime)
        {
            return new DepartureTimeIsBefore(localTime);
        }

        [NotNull]
        public static ISpecification<Flight> ArrivalTimeIsAfter(LocalTime localTime)
        {
            return new ArrivalTimeIsAfter(localTime);
        }

        [NotNull]
        public static ISpecification<Flight> DepartureTimeIsAfter(LocalTime localTime)
        {
            return new DepartureTimeIsAfter(localTime);
        }
    }
}