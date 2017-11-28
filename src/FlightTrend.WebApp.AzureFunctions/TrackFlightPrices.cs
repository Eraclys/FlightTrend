using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Ioc;
using FlightTrend.Core.Models;
using FlightTrend.Core.Repositories;
using FlightTrend.Queries;
using FlightTrend.Register;
using JetBrains.Annotations;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using NodaTime;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightTrend.WebApp.AzureFunctions
{
    public static class TrackFlightPrices
    {
        [FunctionName("TrackFlightPrices")]
        public static async Task RunAsync([TimerTrigger("0 0 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            var dependencyResolver = Ioc.Bootstrap(new FlightTrendConfig());
            var cheapestReturnFlightsRepository = dependencyResolver.GetService<ICheapestReturnFlightsRepository>();
            var cheapestFlightFinder = dependencyResolver.GetService<ICheapestFlightFinder>();
            var clock = dependencyResolver.GetService<IClock>();

            var archives = await cheapestReturnFlightsRepository.Get();
            var newReturnFlights = await FetchUpdatedReturnFlights(cheapestFlightFinder, clock);

            await cheapestReturnFlightsRepository.Save(archives.Concat(newReturnFlights));
        }

        [ItemNotNull]
        private static async Task<IEnumerable<ReturnFlightArchive>> FetchUpdatedReturnFlights(
            ICheapestFlightFinder _flightFinder,
            IClock _clock)
        {
            var criteria = Criteria.DefaultCriteria();

            var returnFlights = await _flightFinder.FindCheapestReturnFlightsForMultipleTravelDates(criteria);

            var currentInstant = _clock.GetCurrentInstant();

            return returnFlights.Select(x => new ReturnFlightArchive(currentInstant, x));
        }
    }
}
