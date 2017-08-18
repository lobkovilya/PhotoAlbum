using System.Web.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using PhotoAlbum.Client;
using PhotoAlbum.Client.Utils;

namespace PhotoAlbum.WebAPI
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<WebApiPhotoAlbumProxy>().As<IPhotoAlbumProxy>()
                .WithParameter("baseAddress", WebConfigurationManager.AppSettings["WebApiHost"]);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }
    }
}