using FlightTrend.Core.FlightFinders;
using FlightTrend.Core.Ioc;
using FlightTrend.WebApp.Controllers;
using JetBrains.Annotations;
using System;
using System.Web.Mvc;
using FlightTrend.Core.Repositories;
using Microsoft.Extensions.Caching.Memory;
using NodaTime;
using IDependencyResolver = FlightTrend.Core.Ioc.IDependencyResolver;

namespace FlightTrend.WebApp
{
    internal sealed class FlightTrendControllerFactory : DefaultControllerFactory
    {
        private readonly IDependencyResolver _dependencyResolver;

        public FlightTrendControllerFactory(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        [CanBeNull]
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == typeof(HomeController))
            {
                return new HomeController(
                    _dependencyResolver.GetService<ICheapestFlightFinder>(),
                    _dependencyResolver.GetService<MemoryCache>(),
                    _dependencyResolver.GetService<ICheapestReturnFlightsRepository>(),
                    _dependencyResolver.GetService<IClock>());
            }

            return null;
        }
    }
}