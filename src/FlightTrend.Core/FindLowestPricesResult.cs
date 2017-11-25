using System;
using NodaTime;
using System.Collections.Generic;

namespace FlightTrend.Core
{
    public sealed class FindLowestPricesResult
    {
        public FindLowestPricesResult(IEnumerable<FlightPrice> departurePrices, IEnumerable<FlightPrice> returnPrices)
        {
            DeparturePrices = departurePrices;
            ReturnPrices = returnPrices;
        }

        public IEnumerable<FlightPrice> DeparturePrices { get; }
        public IEnumerable<FlightPrice> ReturnPrices { get; }
    }

    public sealed class FlightPrice : IEquatable<FlightPrice>
    {
        public FlightPrice(
            LocalDate departureDate,
            LocalDate arrivalDate,
            LocalTime departureTime,
            LocalTime arrivalTime,
            decimal price)
        {
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Price = price;
        }

        public LocalDate DepartureDate { get; }
        public LocalDate ArrivalDate { get; }
        public LocalTime DepartureTime { get; }
        public LocalTime ArrivalTime { get; }
        public decimal Price { get; }

        public bool Equals(FlightPrice other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return DepartureDate.Equals(other.DepartureDate) && ArrivalDate.Equals(other.ArrivalDate) && DepartureTime.Equals(other.DepartureTime) && ArrivalTime.Equals(other.ArrivalTime) && Price == other.Price;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj is FlightPrice && Equals((FlightPrice) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DepartureDate.GetHashCode();
                hashCode = (hashCode * 397) ^ ArrivalDate.GetHashCode();
                hashCode = (hashCode * 397) ^ DepartureTime.GetHashCode();
                hashCode = (hashCode * 397) ^ ArrivalTime.GetHashCode();
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(FlightPrice left, FlightPrice right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FlightPrice left, FlightPrice right)
        {
            return !Equals(left, right);
        }
    }
}