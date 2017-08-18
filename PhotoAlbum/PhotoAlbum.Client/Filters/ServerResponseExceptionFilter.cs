using System.Web.Mvc;
using PhotoAlbum.Client.Infrastructure;

namespace PhotoAlbum.Client.Filters
{
    public class ServerResponseExceptionFilter : FilterAttribute, IExceptionFilter
    {

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !(filterContext.Exception is ServerResponseException))
            {
                return;
            }
            
            filterContext.ExceptionHandled = true;

            var e = (ServerResponseException) filterContext.Exception;

            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(e.StatusCode);
                return;
            }

            filterContext.HttpContext.Response.StatusCode = (int)e.StatusCode;
            filterContext.Result = new ViewResult
            {
                ViewName = "ErrorPage",
                ViewData = new ViewDataDictionary(e.ReasonPhrase)
            };

            
        }
    }
}