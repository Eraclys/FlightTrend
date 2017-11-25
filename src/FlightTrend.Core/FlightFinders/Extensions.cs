using System.Collections.Generic;
using System.Linq;
using FlightTrend.Core.Models;

namespace FlightTrend.Core.FlightFinders
{
    public static class Extensions
    {
        public static Flight GetCheapestFlight(this IEnumerable<Flight> flights)
        {
            return flights.Aggregate((Flight)null, (a, b) => a?.Price < b?.Price ? a : b);
        }

        public static ReturnFlight GetCheapestReturnFlight(this IEnumerable<ReturnFlight> returnFlights)
        {
            return returnFlights.Aggregate((ReturnFlight)null, (a, b) => a?.TotalPrice < b?.TotalPrice ? a : b);
        }
    }
}