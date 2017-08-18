using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using log4net;
using PhotoAlbum.BLL.Contracts.Infrastructure;
using PhotoAlbum.WebAPI.Utils;

namespace PhotoAlbum.WebAPI.Filters
{
    public class ValidationExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ValidationException)
            {
                var e = (ValidationException) context.Exception;

                var dict = new ModelStateDictionary();
                dict.AddModelErrors(e.ModelErrors);
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, dict);
            }
        }
    }
}