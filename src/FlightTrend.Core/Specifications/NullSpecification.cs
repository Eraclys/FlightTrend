using JetBrains.Annotations;

namespace FlightTrend.Core.Specifications
{

    [UsedImplicitly]
    public sealed class NullSpecification<T> : ISpecification<T>
    {
        public bool IsSatisfiedBy(T value)
        {
            return true;
        }
    }
}