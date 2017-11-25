using FlightTrend.Core.Models;
using FlightTrend.Core.Specifications;
using FlightTrend.Core.Validation;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Core.FlightFinders
{
    [UsedImplicitly]
    public sealed class FindCheapestReturnFlightCriteria
    {
        public FindCheapestReturnFlightCriteria(
            [NotNull] string fromAirport,
            [NotNull] string toAirport,
            LocalDate departureDate,
            LocalDate returnDate,
            [CanBeNull] ISpecification<Flight> departureFlightsFilter,
            [CanBeNull] ISpecification<Flight> returnFlightsFilter)
        {
            Guard.MustNotBeNullOrWhiteSpace(fromAirport, nameof(fromAirport));
            Guard.MustNotBeNullOrWhiteSpace(toAirport, nameof(toAirport));

            FromAirport = fromAirport;
            ToAirport = toAirport;
            DepartureDate = departureDate;
            ReturnDate = returnDate;
            DepartureFlightsFilter = departureFlightsFilter ?? new NullSpecification<Flight>();
            ReturnFlightsFilter = returnFlightsFilter ?? new NullSpecification<Flight>();
        }

        public string FromAirport { get; }
        public string ToAirport { get; }
        public LocalDate DepartureDate { get; }
        public LocalDate ReturnDate { get; }
        public ISpecification<Flight> DepartureFlightsFilter { get; }
        public ISpecification<Flight> ReturnFlightsFilter { get; }
    }
}