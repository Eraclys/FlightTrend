using System.Collections.Generic;

namespace FlightTrend.PegasusAirlines
{
    public class PegasusApiFlightSearchRequest
    {
        public List<FlightSearchItem> FlightSearchList { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
        public int SoldierCount { get; set; }
        public string Currency { get; set; }
        public int DateOption { get; set; }
        public bool FfRedemption { get; set; }
        public bool OpenFlightSearch { get; set; }
        public bool PersonnelFlightSearch { get; set; }
        public string OperationCode { get; set; }
        public AffiliateContent Affiliate { get; set; }

        public class FlightSearchItem
        {
            public string DeparturePort { get; set; }
            public string ArrivalPort { get; set; }
            public string DepartureDate { get; set; }
            public string ReturnDate { get; set; }
        }

        public class AffiliateContent
        {
        }
    }
}
