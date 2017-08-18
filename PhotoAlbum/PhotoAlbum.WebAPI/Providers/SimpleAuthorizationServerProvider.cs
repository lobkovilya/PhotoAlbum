using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using PhotoAlbum.BLL.Interfaces;
using PhotoAlbum.BLL.Services;
using PhotoAlbum.BLL.Contracts.Infrastructure;
using PhotoAlbum.DAL.EF;
using PhotoAlbum.DAL.Entities;
using PhotoAlbum.DAL.Interfaces;
using PhotoAlbum.DAL.Repositories;

namespace PhotoAlbum.WebAPI.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //private readonly string _connectionString;
        //private readonly IUnitOfWork _database;

        //public SimpleAuthorizationServerProvider(ConnectionStringProvider connectionStringProvider)
        //{
        //    _connectionString = connectionStringProvider.ConnectionString;
        //}

        //public SimpleAuthorizationServerProvider(IUnitOfWork database)
        //{
        //    _database = database;
        //}

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var autofacLifetimeScope = context.OwinContext.GetAutofacLifetimeScope();
            var database = autofacLifetimeScope.Resolve<IUnitOfWork>();

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //var dbContext = new PhotoAlbumContext(_connectionString);

            //var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(dbContext));

            var user = await database.UserManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("name", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }
    }
}