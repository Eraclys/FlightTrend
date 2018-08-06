using FlightTrend.Core.Models;
using JetBrains.Annotations;

namespace FlightTrend.Core.Specifications
{
    [UsedImplicitly]
    public sealed class DepartureDateIsExact : ISpecification<Flight>
    {
        public bool IsSatisfiedBy(Flight value)
        {
            return value.DepartureDate  == value.RequestedDate;
        }
    }
}
