using JetBrains.Annotations;

namespace FlightTrend.Core.Specifications
{
    [UsedImplicitly]
    public sealed class NotSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _innerSpec;

        public NotSpecification(ISpecification<T>  innerSpec)
        {
            _innerSpec = innerSpec;
        }

        public bool IsSatisfiedBy(T value)
        {
            return !_innerSpec.IsSatisfiedBy(value);
        }
    }
}