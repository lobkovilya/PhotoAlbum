using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using PhotoAlbum.Client.Infrastructure;
using PhotoAlbum.BLL.Contracts.Infrastructure;

namespace PhotoAlbum.Client.Filters
{
    public class AuthorizationExceptionFilter : FilterAttribute, IExceptionFilter
    {

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !(filterContext.Exception is AuthorizationException))
            {
                return;
            }
            filterContext.ExceptionHandled = true;

            var e = (AuthorizationException)filterContext.Exception;

            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                return;
            }

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"controller", "Account"},
                    {"action", "Login" }
                });
        }
    }
}