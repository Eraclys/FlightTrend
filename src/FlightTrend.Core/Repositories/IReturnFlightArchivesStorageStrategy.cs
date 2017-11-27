using FlightTrend.Core.Models;
using JetBrains.Annotations;
using NodaTime;
using System.Collections.Generic;
using System.Linq;

namespace FlightTrend.Core.Repositories
{
    public interface IReturnFlightArchivesStorageStrategy
    {
        [NotNull]
        IEnumerable<ReturnFlightArchive> Optimize(IEnumerable<ReturnFlightArchive> records);
    }

    public sealed class OnlyStoreChangesStorageStrategy : IReturnFlightArchivesStorageStrategy
    {
        public IEnumerable<ReturnFlightArchive> Optimize(IEnumerable<ReturnFlightArchive> records)
        {
            return records
                .GroupBy(x => x.ReturnFlight)
                .Select(g => new ReturnFlightArchive(g.Select(x => x.Instant).Min(), g.Key));
        }
    }

    public sealed class OnlyLastYearWorthOfData : IReturnFlightArchivesStorageStrategy
    {
        private readonly IClock _clock;

        public OnlyLastYearWorthOfData(IClock clock)
        {
            _clock = clock;
        }

        public IEnumerable<ReturnFlightArchive> Optimize(IEnumerable<ReturnFlightArchive> records)
        {
            var currentInstant = _clock.GetCurrentInstant();

            return records
                .Where(x => (currentInstant - x.Instant).TotalDays < 365);
        }
    }

    public sealed class CompositeStorageStrategy : IReturnFlightArchivesStorageStrategy
    {
        private readonly IReturnFlightArchivesStorageStrategy[] _storageStrategies;

        public CompositeStorageStrategy(params IReturnFlightArchivesStorageStrategy[] storageStrategies)
        {
            _storageStrategies = storageStrategies;
        }

        public IEnumerable<ReturnFlightArchive> Optimize(IEnumerable<ReturnFlightArchive> records)
        {
            return _storageStrategies
                .Aggregate(records, (current, storageStrategy) => storageStrategy.Optimize(current));
        }
    }
}
