using FlightTrend.Core.Models;
using FlightTrend.Core.Specifications;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Core.FlightFinders
{
    public sealed class FindCheapestReturnFlightCriteriaBuilder
    {
        private string _fromAirport;
        private string _toAirport;
        private LocalDate _departureDate;
        private LocalDate _returnDate;
        private ISpecification<Flight> _departureFlightsFilter;
        private ISpecification<Flight> _returnFlightsFilter;

        [NotNull]
        public static FindCheapestReturnFlightCriteriaBuilder New()
        {
            return new FindCheapestReturnFlightCriteriaBuilder();
        }

        [NotNull]
        public FindCheapestReturnFlightCriteria Build()
        {
            return new FindCheapestReturnFlightCriteria(
                _fromAirport,
                _toAirport,
                _departureDate,
                _returnDate,
                _departureFlightsFilter,
                _returnFlightsFilter);
        }

        [NotNull]
        public  FindCheapestReturnFlightCriteriaBuilder From(string fromAirport)
        {
            _fromAirport = fromAirport;
            return this;
        }

        [NotNull]
        public FindCheapestReturnFlightCriteriaBuilder To(string toAirport)
        {
            _toAirport = toAirport;
            return this;
        }

        [NotNull]
        public FindCheapestReturnFlightCriteriaBuilder Leaving(LocalDate date)
        {
            _departureDate = date;
            return this;
        }

        [NotNull]
        public FindCheapestReturnFlightCriteriaBuilder Returning(LocalDate date)
        {
            _returnDate = date;
            return this;
        }

        [NotNull]
        public FindCheapestReturnFlightCriteriaBuilder FilterDepartureWith(ISpecification<Flight> specification)
        {
            _departureFlightsFilter = specification;
            return this;
        }

        [NotNull]
        public FindCheapestReturnFlightCriteriaBuilder FilterReturnWith(ISpecification<Flight> specification)
        {
            _returnFlightsFilter = specification;
            return this;
        }
    }
}