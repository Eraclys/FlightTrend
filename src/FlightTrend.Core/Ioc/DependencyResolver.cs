using System;
using System.Collections.Concurrent;

namespace FlightTrend.Core.Ioc
{
    public sealed class DependencyResolver : IDependencyResolver
    {
        private bool _isLocked;
        private readonly ConcurrentDictionary<Type, object> _registrations = new ConcurrentDictionary<Type, object>();

        public object GetService(Type serviceType)
        {
            _isLocked = true;

            if (_registrations.TryGetValue(serviceType, out var service))
            {
                return service;
            }

            return null;
        }

        public void RegisterService(Type serviceType, object instance)
        {
            if (_isLocked)
            {
                throw new Exception("Cannot register service at this stage");
            }

            _registrations[serviceType] = instance;
        }
    }
}