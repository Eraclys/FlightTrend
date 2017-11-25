using FlightTrend.Core.Models;
using FlightTrend.Core.Serialization;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightTrend.Serializers
{
    public sealed class ReturnFlightArchiveCollectionSerializer : ISerializer<IEnumerable<ReturnFlightArchive>>
    {
        private readonly ISerializer<ReturnFlightArchive> _itemSerializer;

        public ReturnFlightArchiveCollectionSerializer(ISerializer<ReturnFlightArchive> itemSerializer)
        {
            _itemSerializer = itemSerializer;
        }

        public string Serialize(IEnumerable<ReturnFlightArchive> value)
        {
            return value?.Aggregate(string.Empty, (a, b) => $"{a}{_itemSerializer.Serialize(b)}{Environment.NewLine}");
        }

        [NotNull]
        public IEnumerable<ReturnFlightArchive> Deserialize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Enumerable.Empty<ReturnFlightArchive>();

            return value
                .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => _itemSerializer.Deserialize(x));
        }
    }
}