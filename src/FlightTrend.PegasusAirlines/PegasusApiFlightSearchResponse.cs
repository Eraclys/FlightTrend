using System;
using System.Collections.Generic;

namespace FlightTrend.PegasusAirlines
{
    public class TotalFare
    {
        public string Currency { get; set; }
        public double Amount { get; set; }
    }

    public class Fare
    {
        public TotalFare TotalFare { get; set; }
    }

    public class FlightList
    {
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public Fare Fare { get; set; }
        public bool HasConnectedFlight { get; set; }
        public bool NextDayFlight { get; set; }
    }

    public class DailyFlightList
    {
        public string Date { get; set; }
        public List<FlightList> FlightList { get; set; }
    }

    public class DepartureRouteList
    {
        public List<DailyFlightList> DailyFlightList { get; set; }
    }

    public class ReturnRoute
    {
        public List<DailyFlightList> DailyFlightList { get; set; }
    }

    public class PegasusApiFlightSearchResponse
    {
        public List<DepartureRouteList> DepartureRouteList { get; set; }
        public ReturnRoute ReturnRoute { get; set; }
    }
}
