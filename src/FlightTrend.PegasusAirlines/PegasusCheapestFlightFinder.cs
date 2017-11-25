using FlightTrend.Core.FlightFinders;
using System.Linq;
using System.Threading.Tasks;
using FlightTrend.Core.Models;
using JetBrains.Annotations;

namespace FlightTrend.PegasusAirlines
{
    public sealed class PegasusCheapestFlightFinder : ICheapestFlightFinder
    {
        [ItemCanBeNull]
        public async Task<ReturnFlight> FindCheapestReturnFlight(FindCheapestReturnFlightCriteria criteria)
        {
            var parameters = PegasusApiUtils.GetReturnFlightParameters(criteria);
            var response = await PegasusApiUtils.ExecuteFindReturnFlightsRequest(parameters);
            var results = PegasusApiUtils.ParseReturnFlightResults(response, criteria.FromAirport, criteria.ToAirport).ToList();

            var departureFlight = results
                .Where(x => x.DepartureDate == criteria.DepartureDate && criteria.DepartureFlightsFilter.IsSatisfiedBy(x))
                .GetCheapestFlight();

            var returnFlight = results
                .Where(x => x.DepartureDate == criteria.ReturnDate && criteria.ReturnFlightsFilter.IsSatisfiedBy(x))
                .GetCheapestFlight();

            if (departureFlight != null && returnFlight != null)
            {
                return new ReturnFlight(departureFlight, returnFlight);
            }

            return null;
        }
    }
}
