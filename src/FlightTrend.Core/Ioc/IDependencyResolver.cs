using System;

namespace FlightTrend.Core.Ioc
{
    public interface IDependencyResolver
    {
        object GetService(Type serviceType);
        void RegisterService(Type serviceType, object instance);
    }
}