namespace FlightTrend.Core.Models
{
    public sealed class ReturnFlight
    {
        public ReturnFlight(Flight departure, Flight @return)
        {
            Departure = departure;
            Return = @return;
        }

        public Flight Departure { get; }
        public Flight Return { get; }

        public decimal TotalPrice => Departure.Price + Return.Price;
    }
}