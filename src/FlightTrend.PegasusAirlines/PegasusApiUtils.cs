using FlightTrend.Core.FlightFinders;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlightTrend.Core.Models;
using JetBrains.Annotations;

namespace FlightTrend.PegasusAirlines
{
    public static class PegasusApiUtils
    {
        private const string FindPricesUrl = "https://book.flypgs.com/Common/MemberRezvResults.jsp?activeLanguage=EN HTTP/1.1";

        private static readonly Regex PricesRegex = new Regex(
            @"'-1#\s+([^#]+)(?:(?!fltDateDep).)*[^']+'([^']+)'(?:(?!fltDateArr).)*[^']+'([^']+)'(?:(?!fltTimeDep).)*[^']+'([^']+)'(?:(?!fltTimeArr).)*[^']+'([^']+)'",
            RegexOptions.Compiled | RegexOptions.Multiline);

        [NotNull]
        public static IEnumerable<Flight> ParseReturnFlightResults(string response, string from, string to)
        {
            var prices = new List<Flight>();

            foreach (Match match in PricesRegex.Matches(response))
            {
                var departureDate = match.Groups[2].Value;
                var arrivalDate = match.Groups[3].Value;
                var departureTime = match.Groups[4].Value;
                var arrivalTime = match.Groups[5].Value;
                var amount = match.Groups[1].Value;

                prices.Add(new Flight(
                    "Pegasus",
                    from,
                    to,
                    PegasusDateToLocalDate(departureDate),
                    PegasusTimeToLocalTime(departureTime),
                    PegasusDateToLocalDate(arrivalDate),
                    PegasusTimeToLocalTime(arrivalTime),
                    decimal.Parse(amount)));
            }

            return prices.Distinct();
        }

        [NotNull]
        public static IEnumerable<KeyValuePair<string, string>> GetReturnFlightParameters(FindCheapestReturnFlightCriteria criteria)
        {
            return new Dictionary<string, string>
            {
                {"DEPPORT", criteria.FromAirport},
                {"ARRPORT", criteria.ToAirport},
                {"LBLDEPPORT", ""},
                {"LBLARRPORT", ""},
                {"TRIPTYPE", "R"},
                {"DEPDATE", criteria.DepartureDate.ToDateTimeUnspecified().ToString("dd/MM/yyyy")},
                {"RETDATE", criteria.ReturnDate.ToDateTimeUnspecified().ToString("dd/MM/yyyy")},
                {"ADULT", "1"},
                {"CHILD", "0"},
                {"INFANT", "0"},
                {"STUDENT", "0"},
                {"SOLDIER", "0"},
                {"CURRENCY", "GBP"},
                {"LC", "EN"},
                {"FLEX", ""},
                {"userId", ""},
                {"resetErrors", "T"},
                {"clickedButton", "btnSearch"},
                {"DEPDATEO", ""},
                {"RETDATEO", ""}
            };
        }

        public static async Task<string> ExecuteFindReturnFlightsRequest(IEnumerable<KeyValuePair<string,string>> parameters)
        {
            using (var httpClient = CreateHttpClient())
            {
                var httpResponseMessage = await httpClient.PostAsync(FindPricesUrl, new FormUrlEncodedContent(parameters));
                var response = await httpResponseMessage.Content.ReadAsStringAsync();

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new Exception(response);
                }

                return response;
            }
        }

        public static LocalDate PegasusDateToLocalDate(string date)
        {
            return LocalDate.FromDateTime(DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture));
        }

        public static LocalTime PegasusTimeToLocalTime(string time)
        {
            var timeSpan = TimeSpan.Parse(time);

            return LocalTime.FromHourMinuteSecondMillisecondTick(
                timeSpan.Hours,
                timeSpan.Minutes,
                0,
                0,
                0);
        }

        [NotNull]
        private static HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var httpClient = new HttpClient(handler);

            httpClient.DefaultRequestHeaders.Host = "book.flypgs.com";
            httpClient.DefaultRequestHeaders.Add("Origin", "https://www.flypgs.com");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            httpClient.DefaultRequestHeaders.Add("Referer", "https://www.flypgs.com/en");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8,fr;q=0.7,tr;q=0.6,zh-CN;q=0.5,zh;q=0.4");

            return httpClient;
        }
    }
}