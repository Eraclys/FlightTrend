using FlightTrend.Core.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace FlightTrend.PegasusAirlines.Test
{
    [TestClass]
    public sealed class PegasusApiUtils_ParseReturnFlightResults
    {
        private const string From = "STN";
        private const string To = "SAW";

        [TestMethod]
        public void Should_Parse_Correctly()
        {
            var response = GetResourceAsString("FindCheapestReturnFlightResponse.html");

            var result = PegasusApiUtils.ParseReturnFlightResults(response, From, To).ToList();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().AllBeEquivalentTo(new List<Flight>
            {
                Item("08.12.2017", "12:35", "08.12.2017", "19:25", 81.99f),
                Item("08.12.2017", "23:15", "09.12.2017", "06:05", 96.99f),
                Item("09.12.2017", "10:30", "09.12.2017", "17:20", 66.99f),
                Item("09.12.2017", "12:35", "09.12.2017", "19:25", 92.99f),
                Item("09.12.2017", "23:40", "10.12.2017", "06:35", 110.99f),
                Item("10.12.2017", "10:30", "10.12.2017", "17:20", 77.99f),
                Item("10.12.2017", "12:35", "10.12.2017", "19:25", 81.99f),
                Item("10.12.2017", "23:15", "11.12.2017", "06:05", 81.99f),
                Item("01.12.2017", "10:55", "01.12.2017", "11:55", 94.00f),
                Item("01.12.2017", "21:35", "01.12.2017", "22:35", 57.00f),
                Item("02.12.2017", "08:50", "02.12.2017", "09:50", 53.00f),
                Item("02.12.2017", "10:55", "02.12.2017", "11:55", 53.00f),
                Item("02.12.2017", "21:35", "02.12.2017", "22:35", 46.00f),
                Item("03.12.2017", "08:50", "03.12.2017", "09:50", 39.00f),
                Item("03.12.2017", "10:55", "03.12.2017", "11:55", 94.00f),
                Item("03.12.2017", "21:35", "03.12.2017", "22:35", 98.00f)
            });
        }

        [NotNull]
        private static string GetResourceAsString(string path)
        {
            using (var stream = typeof(PegasusApiUtils_ParseReturnFlightResults).GetTypeInfo().Assembly.GetManifestResourceStream($"FlightTrend.PegasusAirlines.Test.Resources.{path}"))
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        [NotNull]
        private static Flight Item(
            string departureDate,
            string departureTime,
            string arrivalDate,
            string arrivalTime,
            float amount)
        {
            return new Flight(
                "Pegasus",
                From,
                To,
                PegasusApiUtils.PegasusDateToLocalDate(departureDate),
                PegasusApiUtils.PegasusTimeToLocalTime(departureTime),
                PegasusApiUtils.PegasusDateToLocalDate(arrivalDate),
                PegasusApiUtils.PegasusTimeToLocalTime(arrivalTime),
                amount);
        }
    }
}