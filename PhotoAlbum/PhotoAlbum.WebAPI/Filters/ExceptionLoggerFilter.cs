using System.Web.Http.Filters;
using log4net;

namespace PhotoAlbum.WebAPI.Filters
{
    public class ExceptionLoggerFilter : ExceptionFilterAttribute
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ExceptionLoggerFilter));

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var e = actionExecutedContext.Exception;
            Log.Warn(e.Message, e);
        }
    }
}