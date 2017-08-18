using System;
using System.Web.Http;
using Autofac;
using log4net.Config;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PhotoAlbum.BLL.Services;
using PhotoAlbum.BLL.Contracts.Mapping;
using PhotoAlbum.WebAPI.Filters;
using PhotoAlbum.WebAPI.Providers;

[assembly: OwinStartup(typeof(PhotoAlbum.WebAPI.Startup))]
[assembly: XmlConfigurator]
namespace PhotoAlbum.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            AutofacConfig.ConfigureContainer(app, config);

            ConfigureOAuth(app);

            config.Filters.Add(new ExceptionLoggerFilter());

            AutoMapperConfig.Configure(typeof(UserService).Assembly, typeof(Startup).Assembly);

            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };
            
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}