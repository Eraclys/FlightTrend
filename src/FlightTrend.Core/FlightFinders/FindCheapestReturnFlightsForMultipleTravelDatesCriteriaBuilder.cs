using FlightTrend.Core.Models;
using FlightTrend.Core.Specifications;
using JetBrains.Annotations;

namespace FlightTrend.Core.FlightFinders
{
    [UsedImplicitly]
    public sealed class FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder
    {

        private string _fromAirport;
        private string _toAirport;
        private ReturnFlightDates[] _dates;
        private ISpecification<Flight> _departureFlightsFilter;
        private ISpecification<Flight> _returnFlightsFilter;

        private FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder()
        {
            _dates = new ReturnFlightDates[0];
        }

        [NotNull]
        public static FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder New()
        {
            return new FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder();
        }

        [NotNull]
        public FindCheapestReturnFlightsForMultipleTravelDatesCriteria Build()
        {
            return new FindCheapestReturnFlightsForMultipleTravelDatesCriteria(
                _fromAirport,
                _toAirport,
                _departureFlightsFilter,
                _returnFlightsFilter,
                _dates);
        }

        [NotNull]
        public FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder From(string fromAirport)
        {
            _fromAirport = fromAirport;
            return this;
        }

        [NotNull]
        public FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder To(string toAirport)
        {
            _toAirport = toAirport;
            return this;
        }

        [NotNull]
        public FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder TravellingDates(params ReturnFlightDates[] dates)
        {
            _dates = dates;
            return this;
        }

        [NotNull]
        public FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder FilterDepartureWith(ISpecification<Flight> specification)
        {
            _departureFlightsFilter = specification;
            return this;
        }

        [NotNull]
        public FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder FilterReturnWith(ISpecification<Flight> specification)
        {
            _returnFlightsFilter = specification;
            return this;
        }
    }
}