using NodaTime;
using System.Globalization;
using JetBrains.Annotations;

namespace FlightTrend.Core.Extensions
{
    public static class NodaTimeExtensions
    {
        [NotNull]
        public static string ToString(this LocalTime localTime, string pattern)
        {
            return localTime.ToString(pattern, CultureInfo.InvariantCulture);
        }


        [NotNull]
        public static string ToString(this LocalDate localDate, string pattern)
        {
            return localDate.ToString(pattern, CultureInfo.InvariantCulture);
        }
    }
}
