using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlightTrend.Core.Models;
using FlightTrend.Core.Serialization;
using JetBrains.Annotations;

namespace FlightTrend.Core.Repositories
{
    [UsedImplicitly]
    public sealed class DiskCheapestReturnFlightsRepository : ICheapestReturnFlightsRepository
    {
        private readonly string _savePath;
        private readonly ISerializer<IEnumerable<ReturnFlightArchive>> _serializer;

        public DiskCheapestReturnFlightsRepository(
            ISerializer<IEnumerable<ReturnFlightArchive>> serializer,
            string savePath)
        {
            _serializer = serializer;
            _savePath = savePath;
        }

        public Task Append(IEnumerable<ReturnFlightArchive> records)
        {
            return WriteToFile(records, FileMode.Append);
        }

        public Task Save(IEnumerable<ReturnFlightArchive> records)
        {
            return WriteToFile(records, FileMode.Create);
        }

        private async Task WriteToFile(IEnumerable<ReturnFlightArchive> records, FileMode fileMode)
        {
            var directoryInfo = new FileInfo(_savePath).Directory;

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            var serializedRecords = _serializer.Serialize(records);

            using (var fileStream = new FileStream(_savePath, fileMode, FileAccess.Write, FileShare.None))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                await streamWriter.WriteAsync(serializedRecords);
                await streamWriter.FlushAsync();
            }
        }

        public async Task<IEnumerable<ReturnFlightArchive>> Get()
        {
            using (var fileStream = new FileStream(_savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            using (var streamReader = new StreamReader(fileStream))
            {
                var content = await streamReader.ReadToEndAsync();
                return _serializer.Deserialize(content) ?? Enumerable.Empty<ReturnFlightArchive>();
            }
        }
    }
}