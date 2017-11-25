using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FlightTrend.Core.Extensions;
using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Models;
using FlightTrend.Core.Specifications;
using FlightTrend.PegasusAirlines;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Console
{
    internal static class Program
    {
        private static void Main()
        {
            //var departureFlightSpecification = new NullSpecification<Flight>();
            var departureFlightSpecification = new DepartureTimeIsAfter(new LocalTime(21, 00));
            var returnFlightSpecification = new DepartureTimeIsAfter(new LocalTime(21, 00));

            var priceFinder = CreatePriceFinder();

            var returnFlights = priceFinder.FindCheapestReturnFlightsForMultipleTravelDates(FindCheapestReturnFlightsForMultipleTravelDatesCriteriaBuilder.New()
                .From("STN")
                .To("SAW")
                .FilterDepartureWith(departureFlightSpecification)
                .FilterReturnWith(returnFlightSpecification)
                .TravellingDates(EveryWeekendForTheNextYear().ToArray())
                .Build()).Result;

            DisplayHeaders();
            returnFlights.ForEach(DisplayReturnFlight);

            System.Console.Read();
        }

        private static void DisplayHeaders()
        {
            System.Console.WriteLine($"{"", 10}|{"DepDate", 10}|{"DepTime",10}|{"ArrDate",10}|{"ArrTime",10}|{"Price",10}");
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

        private static void DisplayReturnFlight([CanBeNull] ReturnFlight returnFlight)
        {
            if (returnFlight == null)
            {
                System.Console.WriteLine("No flights found matching criteria");
            }
            else
            {
                DisplayPrice("Departure", returnFlight.Departure);
                DisplayPrice("Return", returnFlight.Return);
                System.Console.WriteLine($"{"Total",10}|{returnFlight.TotalPrice,50}");
            }
        }

        private static void DisplayPrice(string label, [NotNull] Flight price)
        {
            System.Console.WriteLine($"{label,10}|{price.DepartureDate.ToString("ddd MMM yy", CultureInfo.InvariantCulture),10}|{price.DepartureTime,10}|{price.ArrivalDate.ToString("ddd MMM yy", CultureInfo.InvariantCulture),10}|{price.ArrivalTime,10}|{price.Price,10}");
        }

        [NotNull]
        private static ICheapestFlightFinder CreatePriceFinder()
        {
            return new PegasusCheapestFlightFinder();
        }
    }
}
