using deepend.business.Component;
using deepend.business.Interface;
using deepend.common;
using deepend.data;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace deepend.api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var container = new Container();

            container.Register<IConfigProvider, ConfigProvider>();
            container.RegisterInitializer<ConfigProvider>(config =>
            {
                config.ConnectionStringSettings = ConfigurationManager.ConnectionStrings;
            });
            container.Register<IChequeComponent, ChequeComponent>();
            container.Register<IChequeData, ChequeData>();
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}
