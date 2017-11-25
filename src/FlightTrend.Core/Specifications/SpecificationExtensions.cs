using JetBrains.Annotations;

namespace FlightTrend.Core.Specifications
{
    [UsedImplicitly]
    public static class SpecificationExtensions
    {
        [NotNull]
        public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> otherSpecification)
        {
            return new AndSpecification<T>(left, otherSpecification);
        }

        [NotNull]
        public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> otherSpecification)
        {
            return new OrSpecification<T>(left, otherSpecification);
        }

        [NotNull]
        public static ISpecification<T> Not<T>(this ISpecification<T> left)
        {
            return new NotSpecification<T>(left);
        }
    }
}