using NodaTime;
using System.Globalization;

namespace FlightTrend.Core.Extensions
{
    public static class NodaTimeExtensions
    {
        public static string ToString(this LocalTime localTime, string pattern)
        {
            return localTime.ToString(pattern, CultureInfo.InvariantCulture);
        }


        public static string ToString(this LocalDate localDate, string pattern)
        {
            return localDate.ToString(pattern, CultureInfo.InvariantCulture);
        }
    }
}
