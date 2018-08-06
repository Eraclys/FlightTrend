using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Models;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlightTrend.PegasusAirlines
{
    public static class PegasusApiUtils
    {
        private const string FindPricesUrl = "https://web.flypgs.com/pegasus/availability";
        
        [NotNull]
        public static IEnumerable<Flight> ParseReturnFlightResults(
            PegasusApiFlightSearchResponse response, 
            string from, 
            string to, 
            LocalDate requestedDepartureDate,
            LocalDate requestedReturnDate)
        {
            var departures = response.DepartureRouteList
                .SelectMany(x => x.DailyFlightList)
                .SelectMany(x => x.FlightList)
                .Select(x => new Flight(
                    "Pegasus",
                    from,
                    to,
                    requestedDepartureDate,
                    LocalDate.FromDateTime(x.DepartureDateTime),
                    PegasusTimeToLocalTime(x.DepartureDateTime.TimeOfDay),
                    LocalDate.FromDateTime(x.ArrivalDateTime),
                    PegasusTimeToLocalTime(x.ArrivalDateTime.TimeOfDay),
                    (float)x.Fare.TotalFare.Amount));

            var returns = response.ReturnRoute.DailyFlightList
                .SelectMany(x => x.FlightList)
                .Select(x => new Flight(
                    "Pegasus",
                    to,
                    from,
                    requestedReturnDate,
                    LocalDate.FromDateTime(x.DepartureDateTime),
                    PegasusTimeToLocalTime(x.DepartureDateTime.TimeOfDay),
                    LocalDate.FromDateTime(x.ArrivalDateTime),
                    PegasusTimeToLocalTime(x.ArrivalDateTime.TimeOfDay),
                    (float)x.Fare.TotalFare.Amount));
            

            return departures.Union(returns).Distinct();
        }

        [NotNull]
        public static PegasusApiFlightSearchRequest GetReturnFlightRequest(FindCheapestReturnFlightCriteria criteria)
        {
            return new PegasusApiFlightSearchRequest
            {

                Affiliate = new PegasusApiFlightSearchRequest.AffiliateContent(),
                FfRedemption = false,
                AdultCount = 1,
                ChildCount = 0,
                InfantCount = 0,
                SoldierCount = 0,
                Currency = "GBP",
                DateOption = 1,
                OpenFlightSearch = false,
                PersonnelFlightSearch = false,
                OperationCode = "TK",
                FlightSearchList = new List<PegasusApiFlightSearchRequest.FlightSearchItem>
                {
                    new PegasusApiFlightSearchRequest.FlightSearchItem
                    {
                        DeparturePort = criteria.FromAirport,
                        ArrivalPort = criteria.ToAirport,
                        DepartureDate = criteria.DepartureDate.ToDateTimeUnspecified().ToString("yyyy-MM-dd"),
                        ReturnDate = criteria.ReturnDate.ToDateTimeUnspecified().ToString("yyyy-MM-dd")
                    }
                }
            };
        }

        public static async Task<PegasusApiFlightSearchResponse> ExecuteFindReturnFlightsRequest([NotNull] HttpClient httpClient, PegasusApiFlightSearchRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.PostAsync(FindPricesUrl, stringContent).ConfigureAwait(false);
            var response = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }

            return JsonConvert.DeserializeObject<PegasusApiFlightSearchResponse>(response);
        }

        public static LocalTime PegasusTimeToLocalTime(TimeSpan time)
        {
            return LocalTime.FromHourMinuteSecondMillisecondTick(
                time.Hours,
                time.Minutes,
                0,
                0,
                0);
        }

        [NotNull]
        public static HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var httpClient = new HttpClient(handler);

            httpClient.DefaultRequestHeaders.Host = "web.flypgs.com";
            httpClient.DefaultRequestHeaders.Add("Origin", "https://web.flypgs.com");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
            httpClient.DefaultRequestHeaders.Add("Referer", "https://web.flypgs.com");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "en");
            httpClient.DefaultRequestHeaders.Add("X-VERSION", "1.0.4");
            httpClient.DefaultRequestHeaders.Add("X-PLATFORM", "web");

            return httpClient;
        }
    }
}