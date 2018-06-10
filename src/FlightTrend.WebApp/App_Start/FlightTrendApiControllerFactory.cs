using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Ioc;
using FlightTrend.WebApp.Controllers;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;

namespace FlightTrend.WebApp
{
    internal class FlightTrendApiControllerFactory : IHttpControllerActivator
    {
        private readonly IDependencyResolver _dependencyResolver;

        public FlightTrendApiControllerFactory(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        [CanBeNull]
        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            if (controllerType == typeof(FlightsController))
            {
                return new FlightsController(
                    _dependencyResolver.GetService<ICheapestFlightFinder>(),
                    _dependencyResolver.GetService<MemoryCache>());
            }

            return null;
        }
    }
}