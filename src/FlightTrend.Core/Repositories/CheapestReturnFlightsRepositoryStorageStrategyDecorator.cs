using System.Collections.Generic;
using System.Threading.Tasks;
using FlightTrend.Core.Models;

namespace FlightTrend.Core.Repositories
{
    public sealed class CheapestReturnFlightsRepositoryStorageStrategyDecorator : ICheapestReturnFlightsRepository
    {
        private readonly ICheapestReturnFlightsRepository _innerRepository;
        private readonly IReturnFlightArchivesStorageStrategy _storageStrategy;

        public CheapestReturnFlightsRepositoryStorageStrategyDecorator(
            ICheapestReturnFlightsRepository innerRepository,
            IReturnFlightArchivesStorageStrategy storageStrategy)
        {
            _innerRepository = innerRepository;
            _storageStrategy = storageStrategy;
        }

        public Task Save(IEnumerable<ReturnFlightArchive> records)
        {
            return _innerRepository.Save(_storageStrategy.Optimize(records));
        }

        public Task<IEnumerable<ReturnFlightArchive>> Get()
        {
            return _innerRepository.Get();
        }
    }
}