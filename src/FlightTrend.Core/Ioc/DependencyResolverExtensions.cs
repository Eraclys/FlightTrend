using JetBrains.Annotations;

namespace FlightTrend.Core.Ioc
{
    public static class DependencyResolverExtensions
    {
        public static void RegisterService<T>([NotNull] this IDependencyResolver resolver, T instance)
        {
            resolver.RegisterService(typeof(T), instance);
        }

        public static T GetService<T>([NotNull] this IDependencyResolver resolver)
        {
            return (T)resolver.GetService(typeof(T));
        }
    }
}