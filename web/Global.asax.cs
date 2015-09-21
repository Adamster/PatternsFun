using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Web.CastleWindsorIoC;

namespace Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // ServiceLocator.RegisterAll();
            NHibernateProfiler.Initialize();

            var container = new WindsorContainer();
            container.Install(new ApplicationCastleInstaller());

            // Create the Controller Factory
            var castleControllerFactory = new CastleControllerFactory(container);

            // Add the Controller Factory into the MVC web request pipeline
            ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);
        }
    }
}