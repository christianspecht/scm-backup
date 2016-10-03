using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScmBackup.Http
{
    /// <summary>
    /// Wrapper for HttpClient
    /// </summary>
    internal class HttpRequest : IHttpRequest
    {
        private ILogger logger;

        public HttpClient HttpClient { get; set; }

        public HttpRequest(ILogger logger)
        {
            this.logger = logger;
            this.HttpClient = new HttpClient();
        }

        public void SetBaseUrl(string url)
        {
            this.HttpClient.BaseAddress = new Uri(url);
        }

        public void AddHeader(string name, string value)
        {
            this.HttpClient.DefaultRequestHeaders.Add(name, value);
        }

        public async Task<HttpResult> Execute(string url)
        {
            var result = new HttpResult();
            var response = await this.HttpClient.GetAsync(url);
            result.Status = response.StatusCode;
            result.Headers = response.Headers;

            if (response.IsSuccessStatusCode)
            {
                result.Content = await response.Content.ReadAsStringAsync();
            }

            return result;
        }
    }
}
