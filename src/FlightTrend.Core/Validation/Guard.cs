using System;
using JetBrains.Annotations;

namespace FlightTrend.Core.Validation
{
    [UsedImplicitly]
    public static class Guard
    {
        [AssertionMethod]
        [ContractAnnotation("value:null => halt")]
        public static void MustNotBeNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentException("Must not be null", parameterName);
            }
        }

        [AssertionMethod]
        [ContractAnnotation("value:null => halt")]
        public static void MustNotBeNullOrWhiteSpace(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Must not be null or white space", parameterName);
            }
        }

        [AssertionMethod]
        [ContractAnnotation("condition:false => halt")]
        public static void Requires(bool condition, string parameterName, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        [AssertionMethod]
        public static void MustBePositive(int value, string parameterName)
        {
            Requires(value >= 0, parameterName, "Must be positive");
        }
    }
}