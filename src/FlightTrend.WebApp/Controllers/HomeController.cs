using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Models;
using FlightTrend.Core.Specifications;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;
using NodaTime;

namespace FlightTrend.WebApp.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly ICheapestFlightFinder _flightFinder;
        private readonly MemoryCache _cache;
        private static readonly string CacheKey = typeof(HomeController).FullName;

        public HomeController(ICheapestFlightFinder flightFinder, MemoryCache cache)
        {
            _flightFinder = flightFinder;
            _cache = cache;
        }

        public async Task<ActionResult> Index()
        {
            var returnFlights = await GetReturnFlights();

            return View(returnFlights);
        }

        private async Task<IEnumerable<ReturnFlight>> GetReturnFlights()
        {
            if (_cache.TryGetValue(CacheKey, out var result))
            {
                return (IEnumerable<ReturnFlight>)result;
            }

            var departureFlightSpecification = new DepartureTimeIsAfter(new LocalTime(21, 00));
            var returnFlightSpecification = new DepartureTimeIsAfter(new LocalTime(21, 00));

            var returnFlights = (await _flightFinder.FindCheapestReturnFlightsForMultipleTravelDates(
                FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder.New()
                    .From("STN")
                    .To("SAW")
                    .FilterDepartureWith(departureFlightSpecification)
                    .FilterReturnWith(returnFlightSpecification)
                    .TravellingDates(EveryWeekendForTheNextYear().ToArray())
                    .Build())).ToList();

            _cache.Set(CacheKey, returnFlights, TimeSpan.FromHours(6));

            return returnFlights;
        }

        [ItemNotNull]
        private static IEnumerable<ReturnFlightDates> EveryWeekendForTheNextYear()
        {
            var startDate = SystemClock.Instance.GetCurrentInstant().InUtc().Date;

            for (var i = 0; i < 52; i++)
            {
                var friday = startDate.Next(IsoDayOfWeek.Friday);
                var sunday = friday.Next(IsoDayOfWeek.Sunday);

                yield return new ReturnFlightDates(friday, sunday);

                startDate = sunday;
            }
        }
    }
}
