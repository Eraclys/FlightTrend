using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Specifications;
using JetBrains.Annotations;
using NodaTime;
using System.Collections.Generic;
using System.Linq;

namespace FlightTrend.Queries
{
    public static class Criteria
    {
        [NotNull]
        public static FindCheapestReturnFlightsForMultipleTravelDatesCriteria DefaultCriteria()
        {
            var departureFlightSpecification = new DepartureTimeIsAfter(new LocalTime(21, 00));
            var returnFlightSpecification = new DepartureTimeIsAfter(new LocalTime(21, 00));

            return FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder.New()
                .From("STN")
                .To("SAW")
                .FilterDepartureWith(departureFlightSpecification)
                .FilterReturnWith(returnFlightSpecification)
                .TravellingDates(EveryWeekendForTheNextFiveMonths().ToArray())
                .Build();
        }

        [ItemNotNull]
        private static IEnumerable<ReturnFlightDates> EveryWeekendForTheNextFiveMonths()
        {
            var startDate = SystemClock.Instance.GetCurrentInstant().InUtc().Date;

            for (var i = 0; i < 22; i++)
            {
                var friday = startDate.Next(IsoDayOfWeek.Friday);
                var sunday = friday.Next(IsoDayOfWeek.Sunday);

                yield return new ReturnFlightDates(friday, sunday);

                startDate = sunday;
            }
        }
    }
}
