using System;
using System.Threading.Tasks;
using FlightTrend.Core.Models;

namespace FlightTrend.Core.FlightFinders
{
    public interface ICheapestFlightFinder : IDisposable
    {
        Task<ReturnFlight> FindCheapestReturnFlight(FindCheapestReturnFlightCriteria criteria);
    }
}
