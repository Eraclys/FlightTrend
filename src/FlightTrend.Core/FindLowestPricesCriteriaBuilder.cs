using NodaTime;

namespace FlightTrend.Core
{
    public sealed class FindLowestPricesCriteriaBuilder
    {
        private string _fromAirport;
        private string _toAirport;
        private LocalDate _departureDate;
        private LocalDate _returnDate;

        public static FindLowestPricesCriteriaBuilder New()
        {
            return new FindLowestPricesCriteriaBuilder();
        }

        public FindLowestPricesCriteria Build()
        {
            return new FindLowestPricesCriteria(
                _fromAirport,
                _toAirport,
                _departureDate,
                _returnDate);
        }

        public  FindLowestPricesCriteriaBuilder From(string fromAirport)
        {
            _fromAirport = fromAirport;
            return this;
        }

        public FindLowestPricesCriteriaBuilder To(string toAirport)
        {
            _toAirport = toAirport;
            return this;
        }

        public FindLowestPricesCriteriaBuilder Leaving(LocalDate date)
        {
            _departureDate = date;
            return this;
        }

        public FindLowestPricesCriteriaBuilder Returning(LocalDate date)
        {
            _returnDate = date;
            return this;
        }
    }
}