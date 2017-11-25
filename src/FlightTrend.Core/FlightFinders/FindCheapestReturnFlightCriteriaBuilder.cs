using FlightTrend.Core.Models;
using FlightTrend.Core.Specifications;
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

        public static FindCheapestReturnFlightCriteriaBuilder New()
        {
            return new FindCheapestReturnFlightCriteriaBuilder();
        }

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

        public  FindCheapestReturnFlightCriteriaBuilder From(string fromAirport)
        {
            _fromAirport = fromAirport;
            return this;
        }

        public FindCheapestReturnFlightCriteriaBuilder To(string toAirport)
        {
            _toAirport = toAirport;
            return this;
        }

        public FindCheapestReturnFlightCriteriaBuilder Leaving(LocalDate date)
        {
            _departureDate = date;
            return this;
        }

        public FindCheapestReturnFlightCriteriaBuilder Returning(LocalDate date)
        {
            _returnDate = date;
            return this;
        }

        public FindCheapestReturnFlightCriteriaBuilder FilterDepartureWith(ISpecification<Flight> specification)
        {
            _departureFlightsFilter = specification;
            return this;
        }

        public FindCheapestReturnFlightCriteriaBuilder FilterReturnWith(ISpecification<Flight> specification)
        {
            _returnFlightsFilter = specification;
            return this;
        }
    }
}