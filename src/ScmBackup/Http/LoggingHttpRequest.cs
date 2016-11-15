using System.Net.Http;
using System.Threading.Tasks;

namespace ScmBackup.Http
{
    /// <summary>
    /// logging decorator for HttpRequest
    /// </summary>
    internal class LoggingHttpRequest : IHttpRequest
    {
        private readonly IHttpRequest request;
        private readonly ILogger logger;

        public LoggingHttpRequest(IHttpRequest request, ILogger logger)
        {
            this.request = request;
            this.logger = logger;
        }

        public HttpClient HttpClient
        {
            get { return this.request.HttpClient; }
            set { this.request.HttpClient = value; }
        }

        public void AddHeader(string name, string value)
        {
            this.request.AddHeader(name, value);
        }

        public async Task<HttpResult> Execute(string url)
        {
            string className = this.GetType().Name;

            this.logger.Log(ErrorLevel.Debug, Resource.GetString("HttpRequest"), url);
            var result = await this.request.Execute(url);

            this.logger.Log(ErrorLevel.Debug, Resource.GetString("HttpHeaders"), result.Headers.ToString());
            this.logger.Log(ErrorLevel.Debug, Resource.GetString("HttpResult"), result.Content);

            return result;
        }

        public void SetBaseUrl(string url)
        {
            this.request.SetBaseUrl(url);
        }
    }
}
