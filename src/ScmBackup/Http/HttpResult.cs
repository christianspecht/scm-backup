using System.Net;

namespace ScmBackup.Http
{
    /// <summary>
    /// return value for HttpRequest
    /// </summary>
    internal class HttpResult
    {
        public string Content { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
