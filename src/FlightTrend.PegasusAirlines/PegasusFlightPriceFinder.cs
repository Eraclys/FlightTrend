using FlightTrend.Core;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTrend.PegasusAirlines
{
    public sealed class PegasusFlightPriceFinder : IFlightPricesFinder
    {
        public async Task<FindLowestPricesResult> FindLowestPrices(FindLowestPricesCriteria criteria)
        {
            var parameters = PegasusApiUtils.GetParameters(criteria);
            var response = await PegasusApiUtils.FindLowestPricesRequest(parameters);
            var results = PegasusApiUtils.ParseLowestPricesResult(response).ToList();

            return new FindLowestPricesResult(
                results.Where(x => x.DepartureDate == criteria.DepartureDate),
                results.Where(x => x.DepartureDate == criteria.ReturnDate));
        }
    }
}
