using System.Web.Mvc;
using log4net;

namespace PhotoAlbum.Client.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ExceptionLoggerAttribute));

        public void OnException(ExceptionContext filterContext)
        {
            var e = filterContext.Exception;
            Log.Warn(e.Message, e);
        }
    }
}