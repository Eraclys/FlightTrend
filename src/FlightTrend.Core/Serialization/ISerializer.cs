using JetBrains.Annotations;

namespace FlightTrend.Core.Serialization
{
    public interface ISerializer<T>
    {
        [CanBeNull]
        string Serialize([CanBeNull]T value);

        [CanBeNull]
        T Deserialize([CanBeNull]string value);
    }
}
