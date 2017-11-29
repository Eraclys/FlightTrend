using System;

namespace FlightTrend.Core.Ioc
{
    public interface IDependencyResolver : IDisposable
    {
        object GetService(Type serviceType);
        void RegisterService(Type serviceType, object instance);
    }
}