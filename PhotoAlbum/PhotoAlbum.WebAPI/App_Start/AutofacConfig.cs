using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PhotoAlbum.BLL.Interfaces;
using PhotoAlbum.BLL.Services;
using PhotoAlbum.BLL.Contracts.Infrastructure;
using PhotoAlbum.DAL.Interfaces;
using PhotoAlbum.DAL.Repositories;
using PhotoAlbum.WebAPI.Providers;

namespace PhotoAlbum.WebAPI
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer(IAppBuilder app, HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ConnectionStringProvider>().WithProperty("ConnectionString", "DefaultConnection");

            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>().InstancePerDependency();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerRequest();
            builder.RegisterType<PhotoService>().As<IPhotoService>().InstancePerRequest();


            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            return container;
        }
    }
}