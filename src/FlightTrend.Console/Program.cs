using FlightTrend.Core;
using FlightTrend.PegasusAirlines;
using NodaTime;

namespace FlightTrend.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var priceFinder = CreatePriceFinder();

            var prices = priceFinder.FindLowestPrices(FindLowestPricesCriteriaBuilder
                .Depart("STN", "SAW", NextFriday)
                .Returning(OnFollowingSunday)
                .Build()).Result;

            DisplayPrices(prices);

            System.Console.Read();
        }

        private static void DisplayPrices(FindLowestPricesResult prices)
        {
            System.Console.WriteLine("Departure:");
            prices.DeparturePrices.ForEach(DisplayPrice);

            System.Console.WriteLine("Return:");
            prices.ReturnPrices.ForEach(DisplayPrice);
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
