using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;
using PhotoAlbum.BLL.Contracts.Mapping;
using PhotoAlbum.Client.Filters;
using PhotoAlbum.WebAPI;

[assembly: XmlConfigurator]
namespace PhotoAlbum.Client
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            AutoMapperConfig.Configure(typeof(MvcApplication).Assembly);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new ExceptionLoggerAttribute());
        }
    }
}
