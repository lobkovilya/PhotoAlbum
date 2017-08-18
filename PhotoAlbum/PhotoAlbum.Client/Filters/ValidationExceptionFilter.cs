using System.Linq;
using System.Web.Mvc;
using log4net;
using PhotoAlbum.Client.Utils;
using PhotoAlbum.BLL.Contracts.Infrastructure;

namespace PhotoAlbum.Client.Filters
{
    public class ValidationExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private readonly string _returnView;

        public ValidationExceptionFilter(string returnView = null)
        {
            _returnView = returnView;
        }


        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !(filterContext.Exception is ValidationException))
            {
                return;
            }
            filterContext.ExceptionHandled = true;

            var e = (ValidationException)filterContext.Exception;

            var modelState = (filterContext.Controller as Controller)?.ModelState;
            modelState?.AddModelErrors(e.ModelErrors);
            
            filterContext.Result = new ViewResult
            {
                ViewName = _returnView ?? filterContext.RouteData.Values["action"] as string,
                TempData = filterContext.Controller.TempData,
                ViewData = filterContext.Controller.ViewData
            };
        }
    }
}
