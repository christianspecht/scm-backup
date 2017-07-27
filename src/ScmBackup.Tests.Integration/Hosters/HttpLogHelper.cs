using ScmBackup.Http;

namespace ScmBackup.Tests.Integration.Hosters
{
    /// <summary>
    /// Helper to test Http requests with logging
    /// </summary>
    internal class HttpLogHelper
    {
        /// <summary>
        /// Returns an IHttpRequest which is set up to log via TestLogger
        /// </summary>
        public static IHttpRequest GetRequest(string logName)
        {
            return new LoggingHttpRequest(new HttpRequest(), new TestLogger(logName));
        }

        /// <summary>
        /// Returns an IHttpRequest which is set up to log via the passed ILogger
        /// </summary>
        public static IHttpRequest GetRequest(ILogger logger)
        {
            return new LoggingHttpRequest(new HttpRequest(), logger);
        }
    }
}
