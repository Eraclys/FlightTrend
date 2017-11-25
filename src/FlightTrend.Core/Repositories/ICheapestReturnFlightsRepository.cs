using FlightTrend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightTrend.Core.Repositories
{
    public interface ICheapestReturnFlightsRepository
    {
        Task Append(IEnumerable<ReturnFlightArchive> records);

        Task Save(IEnumerable<ReturnFlightArchive> records);

        Task<IEnumerable<ReturnFlightArchive>> Get();
    }
}