using NodaTime;

namespace FlightTrend.Core
{
    public sealed class FindLowestPricesCriteria
    {
        public FindLowestPricesCriteria(string fromAirport, string toAirport, LocalDate departureDate, LocalDate returnDate)
        {
            Guard.MustNotBeNullOrWhiteSpace(fromAirport, nameof(fromAirport));
            Guard.MustNotBeNullOrWhiteSpace(toAirport, nameof(toAirport));

            FromAirport = fromAirport;
            ToAirport = toAirport;
            DepartureDate = departureDate;
            ReturnDate = returnDate;
        }

        public string FromAirport { get; }
        public string ToAirport { get; }
        public LocalDate DepartureDate { get; }
        public LocalDate ReturnDate { get; }
    }
}