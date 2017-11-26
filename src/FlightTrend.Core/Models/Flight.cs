using System;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Core.Models
{
    public sealed class Flight : IEquatable<Flight>
    {
        public Flight(
            string company,
            string from,
            string to,
            LocalDate departureDate,
            LocalTime departureTime,
            LocalDate arrivalDate,
            LocalTime arrivalTime,
            float price)
        {
            Company = company;
            From = from;
            To = to;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Price = price;
        }

        public string Company { get; }
        public string From { get; }
        public string To { get; }
        public LocalDate DepartureDate { get; }
        public LocalDate ArrivalDate { get; }
        public LocalTime DepartureTime { get; }
        public LocalTime ArrivalTime { get; }
        public float Price { get; }

        public bool Equals([CanBeNull] Flight other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(Company, other.Company) && string.Equals(From, other.From) && string.Equals(To, other.To) && DepartureDate.Equals(other.DepartureDate) && ArrivalDate.Equals(other.ArrivalDate) && DepartureTime.Equals(other.DepartureTime) && ArrivalTime.Equals(other.ArrivalTime) && Price == other.Price;
        }

        public override bool Equals([CanBeNull] object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj is Flight && Equals((Flight) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Company != null ? Company.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (From != null ? From.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (To != null ? To.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ DepartureDate.GetHashCode();
                hashCode = (hashCode * 397) ^ ArrivalDate.GetHashCode();
                hashCode = (hashCode * 397) ^ DepartureTime.GetHashCode();
                hashCode = (hashCode * 397) ^ ArrivalTime.GetHashCode();
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==([CanBeNull] Flight left, [CanBeNull] Flight right)
        {
            return Equals(left, right);
        }

        public static bool operator !=([CanBeNull] Flight left, [CanBeNull] Flight right)
        {
            return !Equals(left, right);
        }
    }
}