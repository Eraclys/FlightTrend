using System;
using JetBrains.Annotations;
using NodaTime;

namespace FlightTrend.Core.Models
{
    public sealed class ReturnFlightArchive : IEquatable<ReturnFlightArchive>
    {
        public Instant Instant { get; }
        public ReturnFlight ReturnFlight { get; }

        public ReturnFlightArchive(
            Instant instant,
            ReturnFlight returnFlight)
        {
            Instant = instant;
            ReturnFlight = returnFlight;
        }

        public bool Equals([CanBeNull] ReturnFlightArchive other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Instant.Equals(other.Instant) && Equals(ReturnFlight, other.ReturnFlight);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj is ReturnFlightArchive && Equals((ReturnFlightArchive) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Instant.GetHashCode() * 397) ^ (ReturnFlight != null ? ReturnFlight.GetHashCode() : 0);
            }
        }

        public static bool operator ==([CanBeNull] ReturnFlightArchive left, [CanBeNull] ReturnFlightArchive right)
        {
            return Equals(left, right);
        }

        public static bool operator !=([CanBeNull] ReturnFlightArchive left, [CanBeNull] ReturnFlightArchive right)
        {
            return !Equals(left, right);
        }
    }
}
