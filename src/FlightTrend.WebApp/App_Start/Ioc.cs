
using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Ioc;
using FlightTrend.PegasusAirlines;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;

namespace FlightTrend.WebApp
{
    internal class Ioc
    {
        [NotNull]
        public static IDependencyResolver Bootstrap()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            var pegasusCheapestFlightFinder = new PegasusCheapestFlightFinder();

            var multiCheapestFlightFinder = new MultiCheapestFlightFinder(
                pegasusCheapestFlightFinder);


            var dependencyResolver = new DependencyResolver();

            dependencyResolver.RegisterService<ICheapestFlightFinder>(multiCheapestFlightFinder);
            dependencyResolver.RegisterService(memoryCache);

            return dependencyResolver;
        }
    }
}