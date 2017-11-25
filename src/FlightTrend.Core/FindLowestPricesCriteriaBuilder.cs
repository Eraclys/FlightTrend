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
        private int? _adult;
        private int? _child;
        private int? _student;

        public static FindLowestPricesCriteriaBuilder Depart(string fromAirport, string toAirport, LocalDate departureDate)
        {
            var builder = new FindLowestPricesCriteriaBuilder();
            builder._fromAirport = fromAirport;
            builder._toAirport = toAirport;
            builder._departureDate = departureDate;

            return builder;
        }

        public FindLowestPricesCriteria Build()
        {
            var totalPeople = _adult ?? 0 + _child ?? 0 + _student ?? 0;
            var noOneSpecified = totalPeople == 0;

            return new FindLowestPricesCriteria(
                _fromAirport,
                _toAirport,
                _departureDate,
                _returnDate,
                _currency ?? "GBP",
                _adult == null && noOneSpecified ? 1 : _adult ?? 0,
                _child ?? 0,
                _student ?? 0);
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

        public FindLowestPricesCriteriaBuilder NumberOfAdults(int count)
        {
            _adult = count;
            return this;
        }

        public FindLowestPricesCriteriaBuilder NumberOfChildren(int count)
        {
            _child = count;
            return this;
        }

        public FindLowestPricesCriteriaBuilder NumberOfStudents(int count)
        {
            _student = count;
            return this;
        }
    }
}