using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightTrend.Core.Models;
using JetBrains.Annotations;

namespace FlightTrend.Core.FlightFinders
{
    [UsedImplicitly]
    public static class Extensions
    {
        public static Flight GetCheapestFlight([NotNull] this IEnumerable<Flight> flights)
        {
            return flights.Aggregate((Flight)null, (a, b) => a?.Price < b?.Price ? a : b);
        }

        internal static ReturnFlight GetCheapestReturnFlight([NotNull] this IEnumerable<ReturnFlight> returnFlights)
        {
            return returnFlights.Aggregate((ReturnFlight)null, (a, b) => a?.TotalPrice < b?.TotalPrice ? a : b);
        }

        [ItemNotNull]
        public static async Task<IEnumerable<ReturnFlight>> FindCheapestReturnFlightsForMultipleTravelDates([NotNull] this ICheapestFlightFinder cheapestFlightFinder, [NotNull] FindCheapestReturnFlightsForMultipleTravelDatesCriteria criteria)
        {
            var tasks = criteria.ReturnFlightDates
                .Select(x => new FindCheapestReturnFlightCriteria(criteria.FromAirport, criteria.ToAirport, x.DepartureDate, x.ReturnDate, criteria.DepartureFlightsFilter, criteria.ReturnFlightsFilter))
                .Select(cheapestFlightFinder.FindCheapestReturnFlight);

            var results = await Task.WhenAll(tasks);

            return results.Where(x => x != null);
        }
    }
}