using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FlightTrend.Register;
using IDependencyResolver = FlightTrend.Core.Ioc.IDependencyResolver;

namespace FlightTrend.WebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private IDependencyResolver _dependencyResolver;

        protected void Application_Start()
        {
            var config = new FlightTrendConfig().LoadFromWebConfig();

            _dependencyResolver = Ioc.Bootstrap(config);

            ControllerBuilder.Current.SetControllerFactory(new FlightTrendControllerFactory(_dependencyResolver));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new FlightTrendApiControllerFactory(_dependencyResolver));
        }

        protected void Application_End()
        {
            _dependencyResolver.Dispose();
        }
    }
}
