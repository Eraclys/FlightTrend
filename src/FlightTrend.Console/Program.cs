using System;
using System.Linq;
using FlightTrend.Core;
using FlightTrend.Core.Specifications;
using FlightTrend.PegasusAirlines;
using NodaTime;

namespace FlightTrend.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var priceFinder = CreatePriceFinder();

            var prices = priceFinder.FindLowestPrices(FindLowestPricesCriteriaBuilder.New()
                .From("STN")
                .To("SAW")
                .LeavingOn(NextFriday)
                .ReturningOn(OnFollowingSunday)
                .Build()).Result;

            //var departureFlightSpecification = new NullSpecification<FlightPrice>();
            var departureFlightSpecification = new DepartureTimeIsAfter(new LocalTime(21, 00));
            var returnFlightSpecification = new DepartureTimeIsAfter(new LocalTime(21, 00));

            DisplayCheapestPrices(prices, departureFlightSpecification, returnFlightSpecification);

            System.Console.Read();
        }

        private static void DisplayCheapestPrices(FindLowestPricesResult prices, ISpecification<FlightPrice> departureFlightSpecification, ISpecification<FlightPrice> returnFlightSpecification)
        {
            var departure = prices.DeparturePrices
                .Where(departureFlightSpecification.IsSatisfiedBy)
                .Aggregate(null, SmallestCost());

            var arrival = prices.ReturnPrices
                .Where(returnFlightSpecification.IsSatisfiedBy)
                .Aggregate(null, SmallestCost());

            if (departure == null ||  arrival == null)
            {
                System.Console.WriteLine("No flights found matching criteria");
            }
            else
            {
                System.Console.WriteLine("Departure:");
                DisplayPrice(departure);

                System.Console.WriteLine("Return:");
                DisplayPrice(arrival);

                System.Console.WriteLine($"Total Cost: {departure.Price + arrival.Price} GBP");
            }
        }

        private static Func<FlightPrice, FlightPrice, FlightPrice> SmallestCost()
        {
            return (a, b) => a?.Price < b?.Price ? a : b;
        }

        private static void DisplayPrice(FlightPrice price)
        {
            System.Console.WriteLine($"dep={price.DepartureDate} {price.DepartureTime} arr={price.ArrivalDate} {price.ArrivalTime} => {price.Price}");
        }

        private static IFlightPricesFinder CreatePriceFinder()
        {
            return new PegasusFlightPriceFinder();
        }

        private static LocalDate NextFriday => SystemClock.Instance.GetCurrentInstant().InUtc().Date.Next(IsoDayOfWeek.Friday);
        private static LocalDate OnFollowingSunday => NextFriday.Next(IsoDayOfWeek.Sunday);
    }
}
