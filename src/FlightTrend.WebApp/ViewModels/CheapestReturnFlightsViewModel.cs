using FlightTrend.Core.Models;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.WebApp.ViewModels
{
    public sealed class CheapestReturnFlightsViewModel
    {
        public IEnumerable<Item> Items { get; }

        public CheapestReturnFlightsViewModel(
            [NotNull] IEnumerable<ReturnFlightArchive> archives,
            [NotNull] IEnumerable<ReturnFlightArchive> newReturnFlights)
        {
            Items = newReturnFlights
                .Select(x => new Item(
                    x.ReturnFlight,
                    archives.Where(a => a.IsSameReturnFlight(x)).Select(r => new PriceArchive(r.Instant, r.ReturnFlight.TotalPrice))));
        }

        public sealed class Item
        {
            public ReturnFlight ReturnFlight { get; }
            public IEnumerable<PriceArchive> PriceArchives { get; }

            public Item(ReturnFlight returnFlight, IEnumerable<PriceArchive> priceArchives)
            {
                ReturnFlight = returnFlight;
                PriceArchives = priceArchives;
            }
        }

        public sealed class PriceArchive
        {
            public Instant Instant { get; }
            public float Price { get; }

            public PriceArchive(Instant instant, float price)
            {
                Instant = instant;
                Price = price;
            }
        }
    }
}