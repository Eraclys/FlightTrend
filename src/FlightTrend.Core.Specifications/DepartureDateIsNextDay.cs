using FlightTrend.Core.Models;
using JetBrains.Annotations;

namespace FlightTrend.Core.Specifications
{
    [UsedImplicitly]
    public sealed class DepartureDateIsNextDay : ISpecification<Flight>
    {
        public bool IsSatisfiedBy(Flight value)
        {
            return value.DepartureDate == value.RequestedDate.PlusDays(1);
        }
    }
}