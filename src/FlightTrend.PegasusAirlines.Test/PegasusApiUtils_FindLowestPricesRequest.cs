using System;
using System.Threading.Tasks;
using FlightTrend.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;

// ReSharper disable InconsistentNaming

namespace FlightTrend.PegasusAirlines.Test
{
    [TestClass, Ignore]
    public sealed class PegasusApiUtils_FindLowestPricesRequest
    {
        [TestMethod]
        public async Task Should_Return_A_Response()
        {
            var criteria = FindLowestPricesCriteriaBuilder
                .Depart("STN", "SAW", OneWeekFromNow)
                .Returning(TwoWeeksFromNow)
                .Build();

            var parameters = PegasusApiUtils.GetParameters(criteria);

            var response = await PegasusApiUtils.FindLowestPricesRequest(parameters);

            response.Should().NotBeNullOrWhiteSpace();
        }

        private static LocalDate TwoWeeksFromNow => LocalDate.FromDateTime(DateTime.UtcNow.Date.AddDays(7));

        private static LocalDate OneWeekFromNow => TwoWeeksFromNow.PlusDays(7);
    }
}
