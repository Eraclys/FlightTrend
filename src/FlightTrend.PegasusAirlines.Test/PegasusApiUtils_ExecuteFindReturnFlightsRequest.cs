using FlightTrend.Core.FlightFinders;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;
using System;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace FlightTrend.PegasusAirlines.Test
{
    [TestClass, Ignore]
    public sealed class PegasusApiUtils_ExecuteFindReturnFlightsRequest
    {
        [TestMethod]
        public async Task Should_Return_A_Response()
        {
            var criteria = FindCheapestReturnFlightCriteriaBuilder.New()
                .From("STN")
                .To("SAW")
                .Leaving(OneWeekFromNow)
                .Returning(TwoWeeksFromNow)
                .Build();

            var parameters = PegasusApiUtils.GetReturnFlightParameters(criteria);
            var httpClient = PegasusApiUtils.CreateHttpClient();

            var response = await PegasusApiUtils.ExecuteFindReturnFlightsRequest(httpClient, parameters);

            response.Should().NotBeNullOrWhiteSpace();
        }

        private static LocalDate TwoWeeksFromNow => LocalDate.FromDateTime(DateTime.UtcNow.Date.AddDays(7));

        private static LocalDate OneWeekFromNow => TwoWeeksFromNow.PlusDays(7);
    }
}
