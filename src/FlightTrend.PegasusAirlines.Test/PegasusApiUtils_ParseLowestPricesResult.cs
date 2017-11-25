using System.Collections.Generic;
using System.Linq;
using FlightTrend.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable InconsistentNaming

namespace FlightTrend.PegasusAirlines.Test
{
    [TestClass]
    public sealed class PegasusApiUtils_ParseLowestPricesResult
    {
        [TestMethod]
        public void Should_Parse_Correctly()
        {
            var response = Resource.FindLowestPricesResponse;

            var result = PegasusApiUtils.ParseLowestPricesResult(response)?.ToList();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.ShouldBeEquivalentTo(new List<FlightPrice>
            {
                Item("08.12.2017", "12:35", "08.12.2017", "19:25", 81.99),
                Item("08.12.2017", "23:15", "09.12.2017", "06:05", 96.99),
                Item("09.12.2017", "10:30", "09.12.2017", "17:20", 66.99),
                Item("09.12.2017", "12:35", "09.12.2017", "19:25", 92.99),
                Item("09.12.2017", "23:40", "10.12.2017", "06:35", 110.99),
                Item("10.12.2017", "10:30", "10.12.2017", "17:20", 77.99),
                Item("10.12.2017", "12:35", "10.12.2017", "19:25", 81.99),
                Item("10.12.2017", "23:15", "11.12.2017", "06:05", 81.99),
                Item("01.12.2017", "10:55", "01.12.2017", "11:55", 94.00),
                Item("01.12.2017", "21:35", "01.12.2017", "22:35", 57.00),
                Item("02.12.2017", "08:50", "02.12.2017", "09:50", 53.00),
                Item("02.12.2017", "10:55", "02.12.2017", "11:55", 53.00),
                Item("02.12.2017", "21:35", "02.12.2017", "22:35", 46.00),
                Item("03.12.2017", "08:50", "03.12.2017", "09:50", 39.00),
                Item("03.12.2017", "10:55", "03.12.2017", "11:55", 94.00),
                Item("03.12.2017", "21:35", "03.12.2017", "22:35", 98.00)
            });
        }

        private static FlightPrice Item(
            string departureDate,
            string departureTime,
            string arrivalDate,
            string arrivalTime,
            double amount)
        {
            return new FlightPrice(
                "Pegasus",
                PegasusApiUtils.PegasusDateToLocalDate(departureDate),
                PegasusApiUtils.PegasusDateToLocalDate(arrivalDate),
                PegasusApiUtils.PegasusTimeToLocalTime(departureTime),
                PegasusApiUtils.PegasusTimeToLocalTime(arrivalTime),
                (decimal) amount);
        }
    }
}