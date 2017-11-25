using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Core.FlightFinders
{
    [UsedImplicitly]
    public sealed class ReturnFlightDates
    {
        public LocalDate DepartureDate { get; }
        public LocalDate ReturnDate { get; }

        public ReturnFlightDates(LocalDate departureDate, LocalDate returnDate)
        {
            DepartureDate = departureDate;
            ReturnDate = returnDate;
        }
    }
}