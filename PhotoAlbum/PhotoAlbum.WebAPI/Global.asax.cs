using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using PhotoAlbum.BLL.Services;
using PhotoAlbum.BLL.Contracts.Mapping;

namespace PhotoAlbum.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //AutoMapperConfig.Configure(typeof(UserService).Assembly, typeof(WebApiApplication).Assembly);
            //AutofacConfig.ConfigureContainer();
            
        }
    }
}
