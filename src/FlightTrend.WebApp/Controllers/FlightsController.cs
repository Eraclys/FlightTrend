using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FlightTrend.Core.FlightFinders;
using FlightTrend.Queries;
using FlightTrend.WebApp.ViewModels;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;

namespace FlightTrend.WebApp.Controllers
{
    [System.Web.Mvc.Route("api")]
    public sealed class FlightsController : ApiController
    {
        private readonly ICheapestFlightFinder _flightFinder;
        private readonly MemoryCache _cache;
        private static readonly string CacheKey = typeof(FlightsController).FullName;

        public FlightsController(
            ICheapestFlightFinder flightFinder,
            MemoryCache cache)
        {
            _flightFinder = flightFinder;
            _cache = cache;
        }

        [System.Web.Mvc.HttpGet]
        public async Task<IEnumerable<ReturnFlightViewModel>> GetCheapestReturnFlightsViewModel()
        {
            if (_cache.TryGetValue(CacheKey, out var result))
            {
                return (IEnumerable<ReturnFlightViewModel>)result;
            }

            var newReturnFlights = (await FetchUpdatedReturnFlights().ConfigureAwait(true)).ToList();

            _cache.Set(CacheKey, newReturnFlights, TimeSpan.FromMinutes(15));

            return newReturnFlights;
        }

        [ItemNotNull]
        private async Task<IEnumerable<ReturnFlightViewModel>> FetchUpdatedReturnFlights()
        {
            var criteria = Criteria.DefaultCriteria();

            var returnFlights = await _flightFinder.FindCheapestReturnFlightsForMultipleTravelDates(criteria).ConfigureAwait(true);

            return returnFlights.Select(x => new ReturnFlightViewModel
            {
                Departure = x.Departure.DepartureDate.At(x.Departure.DepartureTime).ToDateTimeUnspecified(),
                Return = x.Return.DepartureDate.At(x.Return.DepartureTime).ToDateTimeUnspecified(),
                Price = x.TotalPrice
            });
        }
    }
}
