using System;

namespace FlightTrend.Core
{
    public static class Guard
    {
        public static void MustNotBeNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentException("Must not be null", parameterName);
            }
        }

        public static void MustNotBeNullOrWhiteSpace(string value, string parameterName)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Must not be null or white space", parameterName);
            }
        }

        public static void Requires(bool condition, string parameterName, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        public static void MustBePositive(int value, string parameterName)
        {
            Requires(value >= 0, parameterName, "Must be positive");
        }
    }
}