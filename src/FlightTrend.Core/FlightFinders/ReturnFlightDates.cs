using System;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Core.FlightFinders
{
    [UsedImplicitly]
    public sealed class ReturnFlightDates : IEquatable<ReturnFlightDates>
    {
        public LocalDate DepartureDate { get; }
        public LocalDate ReturnDate { get; }

        public ReturnFlightDates(LocalDate departureDate, LocalDate returnDate)
        {
            DepartureDate = departureDate;
            ReturnDate = returnDate;
        }

        public bool Equals(ReturnFlightDates other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DepartureDate.Equals(other.DepartureDate) && ReturnDate.Equals(other.ReturnDate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is ReturnFlightDates && Equals((ReturnFlightDates) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (DepartureDate.GetHashCode() * 397) ^ ReturnDate.GetHashCode();
            }
        }

        public static bool operator ==(ReturnFlightDates left, ReturnFlightDates right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ReturnFlightDates left, ReturnFlightDates right)
        {
            return !Equals(left, right);
        }
    }
}