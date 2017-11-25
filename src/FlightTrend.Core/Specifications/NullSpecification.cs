namespace FlightTrend.Core.Specifications
{
    public sealed class NullSpecification<T> : ISpecification<T>
    {
        public bool IsSatisfiedBy(T value)
        {
            return true;
        }
    }
}