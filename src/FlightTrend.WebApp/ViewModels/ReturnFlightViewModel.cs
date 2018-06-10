using System;

namespace FlightTrend.WebApp.ViewModels
{
    public sealed class ReturnFlightViewModel
    {
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }
        public double Price { get; set; }
    }
}