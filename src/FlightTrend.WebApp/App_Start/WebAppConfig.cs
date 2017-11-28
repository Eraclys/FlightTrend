using System.Configuration;
using FlightTrend.Register;
using JetBrains.Annotations;

namespace FlightTrend.WebApp
{
    internal static class FlightTrendConfigExtensions
    {
        [NotNull]
        public static FlightTrendConfig LoadFromWebConfig([NotNull] this FlightTrendConfig config)
        {
            config.AzureBlobStorageConnectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            return config;
        }
    }
}