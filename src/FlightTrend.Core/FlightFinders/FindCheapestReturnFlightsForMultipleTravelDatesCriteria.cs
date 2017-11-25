using FlightTrend.Core.Models;
using FlightTrend.Core.Specifications;
using JetBrains.Annotations;

namespace FlightTrend.Core.FlightFinders
{
    [UsedImplicitly]
    public sealed class FindCheapestReturnFlightsForMultipleTravelDatesCriteria
    {
        public ReturnFlightDates[] ReturnFlightDates { get; }
        public string FromAirport { get; }
        public string ToAirport { get; }
        public ISpecification<Flight> DepartureFlightsFilter { get; }
        public ISpecification<Flight> ReturnFlightsFilter { get; }

        public FindCheapestReturnFlightsForMultipleTravelDatesCriteria(string fromAirport, string toAirport, ISpecification<Flight> departureFlightsFilter, ISpecification<Flight> returnFlightsFilter, params ReturnFlightDates[] returnFlightDates)
        {
            FromAirport = fromAirport;
            ToAirport = toAirport;
            DepartureFlightsFilter = departureFlightsFilter;
            ReturnFlightsFilter = returnFlightsFilter;
            ReturnFlightDates = returnFlightDates;
        }
    }
}