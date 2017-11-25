using NodaTime;

namespace FlightTrend.Core.Models
{
    public sealed class ReturnFlightArchive
    {
        public Instant Instant { get; }
        public ReturnFlight ReturnFlight { get; }

        public ReturnFlightArchive(
            Instant instant,
            ReturnFlight returnFlight)
        {
            Instant = instant;
            ReturnFlight = returnFlight;
        }
    }
}
