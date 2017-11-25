namespace FlightTrend.Core.Specifications
{
    public sealed class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public bool IsSatisfiedBy(T value)
        {
            return _left.IsSatisfiedBy(value) && _right.IsSatisfiedBy(value);
        }
    }
}