using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Models;
using FlightTrend.Core.Specifications;
using FlightTrend.PegasusAirlines;
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

            var cheapestPrice = priceFinder.FindCheapestReturnFlight(FindCheapestReturnFlightCriteriaBuilder.New()
                .From("STN")
                .To("SAW")
                .Leaving(NextFriday)
                .Returning(OnFollowingSunday)
                .FilterDepartureWith(departureFlightSpecification)
                .FilterReturnWith(returnFlightSpecification)
                .Build()).Result;

            DisplayCheapestPrices(cheapestPrice);

            System.Console.Read();
        }

        private static void DisplayCheapestPrices(ReturnFlight price)
        {

            if (price == null)
            {
                System.Console.WriteLine("No flights found matching criteria");
            }
            else
            {
                System.Console.WriteLine("Departure:");
                DisplayPrice(price.Departure);

                System.Console.WriteLine("Return:");
                DisplayPrice(price.Return);

                System.Console.WriteLine($"Total Cost: {price.TotalPrice} GBP");
            }
        }

        private static void DisplayPrice(Flight price)
        {
            System.Console.WriteLine($"dep={price.DepartureDate} {price.DepartureTime} arr={price.ArrivalDate} {price.ArrivalTime} => {price.Price}");
        }

        private static ICheapestFlightFinder CreatePriceFinder()
        {
            return new PegasusCheapestFlightFinder();
        }

        private static LocalDate NextFriday => SystemClock.Instance.GetCurrentInstant().InUtc().Date.Next(IsoDayOfWeek.Friday);
        private static LocalDate OnFollowingSunday => NextFriday.Next(IsoDayOfWeek.Sunday);
    }
}
