using NodaTime;

namespace FlightTrend.Core
{
    public sealed class FindLowestPricesCriteria
    {
        public FindLowestPricesCriteria(string fromAirport, string toAirport, LocalDate departureDate, LocalDate? returnDate, string currency, int adult, int child, int student)
        {
            Guard.MustNotBeNullOrWhiteSpace(fromAirport, nameof(fromAirport));
            Guard.MustNotBeNullOrWhiteSpace(toAirport, nameof(toAirport));
            Guard.MustNotBeNullOrWhiteSpace(currency, nameof(currency));
            Guard.MustBePositive(adult, nameof(adult));
            Guard.MustBePositive(child, nameof(child));
            Guard.MustBePositive(student, nameof(student));
            Guard.Requires(adult + child + student > 0, nameof(fromAirport), "Must provide at least one person");

            FromAirport = fromAirport;
            ToAirport = toAirport;
            DepartureDate = departureDate;
            ReturnDate = returnDate;
            Currency = currency;
            Adult = adult;
            Child = child;
            Student = student;
        }

        public string FromAirport { get; }
        public string ToAirport { get; }
        public LocalDate DepartureDate { get; }
        public LocalDate? ReturnDate { get; }
        public string Currency { get; }
        public int Adult { get; }
        public int Child { get; }
        public int Student { get; }
    }
}