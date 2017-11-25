namespace FlightTrend.Core.Specifications
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> otherSpecification)
        {
            return new AndSpecification<T>(left, otherSpecification);
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> otherSpecification)
        {
            return new OrSpecification<T>(left, otherSpecification);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> left)
        {
            return new NotSpecification<T>(left);
        }
    }
}