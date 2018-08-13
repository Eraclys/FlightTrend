using FlightTrend.Core.Models;
using JetBrains.Annotations;
using System.Linq;
using System.Threading.Tasks;
using FlightTrend.Core.Extensions;

namespace FlightTrend.Core.FlightFinders
{
    [UsedImplicitly]
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

            var results = await Task.WhenAll(tasks).ConfigureAwait(false);

            return results.GetCheapestReturnFlight();
        }

        public void Dispose()
        {
            _innerFinders.ForEach(x => x?.Dispose());
        }
    }
}
