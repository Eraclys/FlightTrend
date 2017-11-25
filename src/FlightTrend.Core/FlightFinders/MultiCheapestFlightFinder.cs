using System.Linq;
using System.Threading.Tasks;
using FlightTrend.Core.Models;

namespace FlightTrend.Core.FlightFinders
{
    public sealed class MultiCheapestFlightFinder : ICheapestFlightFinder
    {
        private readonly ICheapestFlightFinder[] _innerFinders;

        public MultiCheapestFlightFinder(params ICheapestFlightFinder[] innerFinders)
        {
            _innerFinders = innerFinders;
        }

        public async Task<ReturnFlight> FindCheapestReturnFlight(FindCheapestReturnFlightCriteria criteria)
        {
            var tasks = _innerFinders.Select(x => x.FindCheapestReturnFlight(criteria));

            var results = await Task.WhenAll(tasks);

            return results.GetCheapestReturnFlight();
        }
    }
}
