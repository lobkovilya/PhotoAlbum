using System.Net;
using System.Web.Mvc;
using log4net;
using PhotoAlbum.BLL.Contracts.Infrastructure;
using PhotoAlbum.Client.Infrastructure;

namespace PhotoAlbum.Client.Filters
{
    public class NotModifiedExceptionFilter : FilterAttribute, IExceptionFilter
    {

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !(filterContext.Exception is NotModifiedException))
            {
                return;
            }

            filterContext.ExceptionHandled = true;

            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NotModified);

        }
    }
}