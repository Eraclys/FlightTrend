using FlightTrend.Core.Models;
using FlightTrend.Core.Repositories;
using FlightTrend.Core.Serialization;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTrend.Repositories.AzureBlobStorage
{
    public sealed class AzureBlobStorageCheapestReturnFlightsRepository : ICheapestReturnFlightsRepository
    {
        private readonly string _connectionString;
        private readonly ISerializer<IEnumerable<ReturnFlightArchive>> _serializer;

        public AzureBlobStorageCheapestReturnFlightsRepository(
            string connectionString,
            ISerializer<IEnumerable<ReturnFlightArchive>> serializer)
        {
            _connectionString = connectionString;
            _serializer = serializer;
        }

        public async Task Save(IEnumerable<ReturnFlightArchive> records)
        {
            var serialized = _serializer.Serialize(records);

            var cloudBlockBlob = await GetBlockBlobReference().ConfigureAwait(false);

            await cloudBlockBlob.UploadTextAsync(serialized).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ReturnFlightArchive>> Get()
        {
            var cloudBlockBlob = await GetBlockBlobReference().ConfigureAwait(false);

            if (await cloudBlockBlob.ExistsAsync().ConfigureAwait(false))
            {
                var serialized = await cloudBlockBlob.DownloadTextAsync().ConfigureAwait(false);

                return _serializer.Deserialize(serialized) ?? Enumerable.Empty<ReturnFlightArchive>();
            }

            return Enumerable.Empty<ReturnFlightArchive>();
        }

        [ItemNotNull]
        private async Task<CloudBlockBlob> GetBlockBlobReference()
        {
            var container = CloudStorageAccount
                .Parse(_connectionString)
                .CreateCloudBlobClient()
                .GetContainerReference("flight-trend");

            await container.CreateIfNotExistsAsync().ConfigureAwait(false);

            var blobReference = container.GetBlockBlobReference("ReturnFlightArchives.sav");

            return blobReference;
        }
    }
}
