using FlightTrend.Core.Models;
using FlightTrend.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace FlightTrend.Serializers
{
    public class ReturnFlightArchiveCollectionSerializer : ISerializer<IEnumerable<ReturnFlightArchive>>
    {
        private readonly ISerializer<ReturnFlightArchive> _itemSerializer;

        public ReturnFlightArchiveCollectionSerializer(ISerializer<ReturnFlightArchive> itemSerializer)
        {
            _itemSerializer = itemSerializer;
        }

        public string Serialize(IEnumerable<ReturnFlightArchive> value)
        {
            return value?.Aggregate(String.Empty, (a, b) => $"{a}{_itemSerializer.Serialize(b)}{Environment.NewLine}");
        }

        [NotNull]
        public IEnumerable<ReturnFlightArchive> Deserialize(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return Enumerable.Empty<ReturnFlightArchive>();
            }

            return value
                .Split(Environment.NewLine.ToCharArray())
                .Select(x => _itemSerializer.Deserialize(x));
        }
    }
}