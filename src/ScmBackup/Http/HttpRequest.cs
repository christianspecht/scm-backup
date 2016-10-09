using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public void AddBasicAuthHeader(string username, string password)
        {
            var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
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
