using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FlightTrend.Register;

namespace FlightTrend.WebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = new FlightTrendConfig().LoadFromWebConfig();

            var dependencyResolver = Ioc.Bootstrap(config);

            ControllerBuilder.Current.SetControllerFactory(new FlightTrendControllerFactory(dependencyResolver));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
