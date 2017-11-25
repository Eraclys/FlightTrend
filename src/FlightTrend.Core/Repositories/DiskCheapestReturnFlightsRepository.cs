using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FlightTrend.Core.Models;
using FlightTrend.Core.Serialization;

namespace FlightTrend.Core.Repositories
{
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
            var serializedRecords = _serializer.Serialize(records);

            using (var fileStream = new FileStream(_savePath, fileMode, FileAccess.Write, FileShare.None))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                await streamWriter.WriteAsync(serializedRecords);
            }
        }

        public async Task<IEnumerable<ReturnFlightArchive>> Get()
        {
            using (var fileStream = new FileStream(_savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            using (var streamReader = new StreamReader(fileStream))
            {
                var content = await streamReader.ReadToEndAsync();
                return _serializer.Deserialize(content);
            }
        }
    }
}