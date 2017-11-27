using System;
using JetBrains.Annotations;

namespace FlightTrend.Core.Models
{
    public sealed class ReturnFlight : IEquatable<ReturnFlight>
    {
        public ReturnFlight(Flight departure, Flight @return)
        {
            Departure = departure;
            Return = @return;
        }

        public Flight Departure { get; }
        public Flight Return { get; }

        public float TotalPrice => Departure.Price + Return.Price;

        public bool Equals([CanBeNull] ReturnFlight other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Departure, other.Departure) && Equals(Return, other.Return);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj is ReturnFlight && Equals((ReturnFlight) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Departure != null ? Departure.GetHashCode() : 0) * 397) ^ (Return != null ? Return.GetHashCode() : 0);
            }
        }

        public static bool operator ==([CanBeNull] ReturnFlight left, [CanBeNull] ReturnFlight right)
        {
            return Equals(left, right);
        }

        public static bool operator !=([CanBeNull] ReturnFlight left, [CanBeNull] ReturnFlight right)
        {
            return !Equals(left, right);
        }
    }
}