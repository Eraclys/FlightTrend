using NodaTime;

namespace FlightTrend.Core
{
    public sealed class FindLowestPricesCriteria
    {
        public FindLowestPricesCriteria(string fromAirport, string toAirport, LocalDate departureDate, LocalDate? returnDate, string currency)
        {
            Guard.MustNotBeNullOrWhiteSpace(fromAirport, nameof(fromAirport));
            Guard.MustNotBeNullOrWhiteSpace(toAirport, nameof(toAirport));
            Guard.MustNotBeNullOrWhiteSpace(currency, nameof(currency));

            FromAirport = fromAirport;
            ToAirport = toAirport;
            DepartureDate = departureDate;
            ReturnDate = returnDate;
            Currency = currency;
        }

        public string FromAirport { get; }
        public string ToAirport { get; }
        public LocalDate DepartureDate { get; }
        public LocalDate? ReturnDate { get; }
        public string Currency { get; }
    }
}