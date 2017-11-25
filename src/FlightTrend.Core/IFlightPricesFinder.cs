using System.Threading.Tasks;

namespace FlightTrend.Core
{
    public interface IFlightPricesFinder
    {
        Task<FindLowestPricesResult> FindLowestPrices(FindLowestPricesCriteria criteria);
    }
}
