using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Ioc;
using FlightTrend.Core.Repositories;
using FlightTrend.PegasusAirlines;
using FlightTrend.Repositories.AzureBlobStorage;
using FlightTrend.Serializers;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;
using NodaTime;

namespace FlightTrend.Register
{
    public class Ioc
    {
        [NotNull]
        public static IDependencyResolver Bootstrap(FlightTrendConfig config)
        {
            var clock = SystemClock.Instance;

            var serializer = new ReturnFlightArchiveCollectionSerializer(
                new ReturnFlightArchiveSerializer(
                    new InstantSerializer(),
                    new LocalDateSerializer(),
                    new LocalTimeSerializer(),
                    new FloatSerializer()));

            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            var pegasusCheapestFlightFinder = new PegasusCheapestFlightFinder();

            var multiCheapestFlightFinder = new MultiCheapestFlightFinder(
                pegasusCheapestFlightFinder);

            var azureRepo = new AzureBlobStorageCheapestReturnFlightsRepository(
                config.AzureBlobStorageConnectionString,
                serializer);

            var repository = new CheapestReturnFlightsRepositoryStorageStrategyDecorator(
                azureRepo,
                new CompositeStorageStrategy(
                    new OnlyLastYearWorthOfData(clock),
                    new OnlyStoreChangesStorageStrategy()));

            var dependencyResolver = new DependencyResolver();

            dependencyResolver.RegisterService<ICheapestFlightFinder>(multiCheapestFlightFinder);
            dependencyResolver.RegisterService(memoryCache);
            dependencyResolver.RegisterService<IClock>(clock);
            dependencyResolver.RegisterService<ICheapestReturnFlightsRepository>(repository);

            return dependencyResolver;
        }
    }
}