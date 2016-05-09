using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Zadania_8.Interfaces;
using Zadania_8.LiteDB;
using Zadania_8.Postgres;
using Zadania_8.Logger;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace Zadania_8
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            builder.RegisterType<SqlPaintingRepository>().As<IPaintingRepository>().InstancePerRequest();
            builder.RegisterType<ArtistRepository>().As<IArtistRepository>().InstancePerRequest();
            builder.RegisterType<Logger.Logger>().As<Logger.ILogger>().SingleInstance();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
