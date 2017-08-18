using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using log4net;

namespace PhotoAlbum.WebAPI.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}