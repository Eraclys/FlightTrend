using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace FlightTrend.Core.Extensions
{
    [UsedImplicitly]
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }
    }
}
