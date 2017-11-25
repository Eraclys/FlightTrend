namespace FlightTrend.Core.Specifications
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T value);
    }
}
