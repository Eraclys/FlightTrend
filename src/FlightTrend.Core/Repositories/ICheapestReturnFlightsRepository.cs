using FlightTrend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace FlightTrend.Core.Repositories
{
    [UsedImplicitly]
    public interface ICheapestReturnFlightsRepository
    {
        [NotNull]
        Task Append(IEnumerable<ReturnFlightArchive> records);

        [NotNull]
        Task Save(IEnumerable<ReturnFlightArchive> records);

        [NotNull]
        [ItemNotNull]
        Task<IEnumerable<ReturnFlightArchive>> Get();
    }
}