using JetBrains.Annotations;

namespace FlightTrend.Core.Models
{
    public static class Extensions
    {

        public static bool IsSameReturnFlight([NotNull] this ReturnFlightArchive a, [NotNull] ReturnFlightArchive b)
        {
            return IsSameFlight(a.ReturnFlight.Departure, b.ReturnFlight.Departure) &&
                   IsSameFlight(a.ReturnFlight.Return, b.ReturnFlight.Return);
        }

        public static bool IsSameFlight([NotNull] this Flight a, [NotNull] Flight b)
        {
            return a.ArrivalDate == b.ArrivalDate &&
                   a.ArrivalTime == b.ArrivalTime &&
                   a.Company == b.Company &&
                   a.DepartureDate == b.DepartureDate &&
                   a.DepartureTime == b.DepartureTime &&
                   a.From == b.From &&
                   a.To == b.To;
        }
    }
}
