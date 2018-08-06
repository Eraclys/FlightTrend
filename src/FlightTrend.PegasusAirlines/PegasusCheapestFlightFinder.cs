using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Models;
using JetBrains.Annotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlightTrend.PegasusAirlines
{
    public sealed class PegasusCheapestFlightFinder : ICheapestFlightFinder
    {
        private readonly HttpClient _httpClient;

        public PegasusCheapestFlightFinder()
        {
            _httpClient = PegasusApiUtils.CreateHttpClient();
        }

        [ItemCanBeNull]
        public async Task<ReturnFlight> FindCheapestReturnFlight(FindCheapestReturnFlightCriteria criteria)
        {
            var request = PegasusApiUtils.GetReturnFlightRequest(criteria);
            var response = await PegasusApiUtils.ExecuteFindReturnFlightsRequest(_httpClient, request).ConfigureAwait(true);
            var results = PegasusApiUtils.ParseReturnFlightResults(
                response, 
                criteria.FromAirport, 
                criteria.ToAirport,
                criteria.DepartureDate,
                criteria.ReturnDate).ToList();

            var departureFlight = results
                .Where(x => x.From == criteria.FromAirport && criteria.DepartureFlightsFilter.IsSatisfiedBy(x))
                .GetCheapestFlight();

            var returnFlight = results
                .Where(x => x.To == criteria.FromAirport && criteria.ReturnFlightsFilter.IsSatisfiedBy(x))
                .GetCheapestFlight();

            if (departureFlight != null && returnFlight != null)
            {
                return new ReturnFlight(departureFlight, returnFlight);
            }

            return null;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
