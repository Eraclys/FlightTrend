using NodaTime;

namespace FlightTrend.Core
{
    public sealed class FindLowestPricesCriteriaBuilder
    {
        private string _fromAirport;
        private string _toAirport;
        private LocalDate _departureDate;
        private LocalDate? _returnDate;
        private string _currency;

        public static FindLowestPricesCriteriaBuilder Depart(string fromAirport, string toAirport, LocalDate departureDate)
        {
            var builder = new FindLowestPricesCriteriaBuilder
            {
                _fromAirport = fromAirport,
                _toAirport = toAirport,
                _departureDate = departureDate
            };

            return builder;
        }

        public FindLowestPricesCriteria Build()
        {
            return new FindLowestPricesCriteria(
                _fromAirport,
                _toAirport,
                _departureDate,
                _returnDate,
                _currency ?? "GBP");
        }

        public FindLowestPricesCriteriaBuilder Returning(LocalDate date)
        {
            _returnDate = date;
            return this;
        }

        public FindLowestPricesCriteriaBuilder UsingCurrency(string currency)
        {
            _currency = currency;
            return this;
        }
    }
}