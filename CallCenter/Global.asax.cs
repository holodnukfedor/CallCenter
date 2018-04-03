using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CallCenterBLL.Infrastructure;
using CallCenter.Infrastructure;
using Ninject.Modules;
using Ninject;
using Ninject.Web.Mvc;

namespace CallCenter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            BLLNinjectBinder.Init(kernel, "CallCenterDb");
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
