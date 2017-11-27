using System.Configuration;
using JetBrains.Annotations;

namespace FlightTrend.WebApp
{
    internal sealed class WebAppConfig
    {
        public string AzureBlobStorageConnectionString { get; set; }
    }

    internal static class WebAppConfigExtensions
    {
        [NotNull]
        public static WebAppConfig LoadFromWebConfig([NotNull] this WebAppConfig config)
        {
            config.AzureBlobStorageConnectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];

            return config;
        }
    }
}