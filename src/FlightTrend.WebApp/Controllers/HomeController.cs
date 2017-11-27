using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Models;
using FlightTrend.Core.Repositories;
using FlightTrend.Core.Specifications;
using FlightTrend.WebApp.ViewModels;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;
using NodaTime;

namespace FlightTrend.WebApp.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly ICheapestFlightFinder _flightFinder;
        private readonly MemoryCache _cache;
        private readonly ICheapestReturnFlightsRepository _cheapestReturnFlightsRepository;
        private readonly IClock _clock;
        private static readonly string CacheKey = typeof(HomeController).FullName;

        public HomeController(
            ICheapestFlightFinder flightFinder,
            MemoryCache cache,
            ICheapestReturnFlightsRepository cheapestReturnFlightsRepository,
            IClock clock)
        {
            _flightFinder = flightFinder;
            _cache = cache;
            _cheapestReturnFlightsRepository = cheapestReturnFlightsRepository;
            _clock = clock;
        }

        public async Task<ActionResult> Index()
        {
            var viewModel = await GetCheapestReturnFlightsViewModel();

            return View(viewModel);
        }

        private async Task<CheapestReturnFlightsViewModel> GetCheapestReturnFlightsViewModel()
        {
            if (_cache.TryGetValue(CacheKey, out var result))
            {
                return (CheapestReturnFlightsViewModel)result;
            }

            var archives = (await _cheapestReturnFlightsRepository.Get()).ToList();
            var newReturnFlights = (await FetchUpdatedReturnFlights()).ToList();

            var viewModel = new CheapestReturnFlightsViewModel(archives, newReturnFlights);

            _cache.Set(CacheKey, viewModel, TimeSpan.FromMinutes(15));

            await _cheapestReturnFlightsRepository.Save(archives.Concat(newReturnFlights));

            return viewModel;
        }

        [ItemNotNull]
        private async Task<IEnumerable<ReturnFlightArchive>> FetchUpdatedReturnFlights()
        {
            var departureFlightSpecification = new DepartureTimeIsAfter(new LocalTime(21, 00));
            var returnFlightSpecification = new DepartureTimeIsAfter(new LocalTime(21, 00));

            var returnFlights = await _flightFinder.FindCheapestReturnFlightsForMultipleTravelDates(
                FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder.New()
                    .From("STN")
                    .To("SAW")
                    .FilterDepartureWith(departureFlightSpecification)
                    .FilterReturnWith(returnFlightSpecification)
                    .TravellingDates(EveryWeekendForTheNextYear().ToArray())
                    .Build());

            var currentInstant = _clock.GetCurrentInstant();

            return returnFlights.Select(x => new ReturnFlightArchive(currentInstant, x));
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
