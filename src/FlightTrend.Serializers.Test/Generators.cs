using System;
using System.Collections.Generic;
using System.Linq;
using FlightTrend.Core.Models;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Serializers.Test
{
    public static class Generators
    {
        private static readonly Random Random = new Random();

        [NotNull]
        public static IEnumerable<ReturnFlightArchive> GenerateListOfReturnFlightArchive()
        {
            return Enumerable.Range(0, Random.Next(0, 30))
                .Select(x => GenerateReturnFlightArchive());
        }

        [NotNull]
        public static ReturnFlightArchive GenerateReturnFlightArchive()
        {
            return new ReturnFlightArchive(
                SystemClock.Instance.GetCurrentInstant(),
                GenerateReturnFlight());
        }

        [NotNull]
        public static ReturnFlight GenerateReturnFlight()
        {
            return new ReturnFlight(GenerateFlight(), GenerateFlight());
        }

        [NotNull]
        public static Flight GenerateFlight()
        {
            return new Flight(RandomString(), RandomString(), RandomString(), RandomDate(), RandomTime(), RandomDate(), RandomTime(), (float)Random.NextDouble());
        }

        public static LocalTime RandomTime()
        {
            return new LocalTime(Random.Next(0, 24), Random.Next(0, 60), Random.Next(0, 60));
        }

        public static LocalDate RandomDate()
        {
            return new LocalDate(Random.Next(0, 3000), Random.Next(1, 13), Random.Next(1, 28));
        }

        public static string RandomString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}