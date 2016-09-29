using Newtonsoft.Json;
using ScmBackup.Http;
using System;
using System.Collections.Generic;

namespace ScmBackup.Hosters
{
    /// <summary>
    /// Calls the GitHub API
    /// </summary>
    internal class GithubApi : IGithubApi
    {
        private readonly IHttpRequest request;
        private readonly ILogger logger;

        public HttpResult LastResult { get; private set; }

        public GithubApi(IHttpRequest request, ILogger logger)
        {
            this.request = request;
            this.logger = logger;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource config)
        {
            var list = new List<HosterRepository>();
            string className = this.GetType().Name;

            // https://developer.github.com/v3/#schema
            request.SetBaseUrl("https://api.github.com");

            // https://developer.github.com/v3/#current-version
            request.AddHeader("Accept", "application/vnd.github.v3+json");

            // https://developer.github.com/v3/#user-agent-required
            request.AddHeader("User-Agent", Resource.GetString("AppTitle"));

            string url = string.Empty;
            switch (config.Type.ToLower())
            {
                case "user":

                    // https://developer.github.com/v3/repos/#list-user-repositories
                    url = string.Format("/users/{0}/repos", config.Name);
                    break;

                case "org":

                    throw new NotImplementedException();

            }

            this.logger.Log(ErrorLevel.Info, Resource.GetString("ApiGettingUrl"), className, request.HttpClient.BaseAddress.ToString() + url);
            this.LastResult = request.Execute(url).Result;

            this.logger.Log(ErrorLevel.Debug, Resource.GetString("ApiResult"), className, this.LastResult.Content);

            var apiResponse = JsonConvert.DeserializeObject<List<GithubApiResponse>>(this.LastResult.Content);
            foreach (var apiRepo in apiResponse)
            {
                var repo = new HosterRepository();
                repo.CloneUrl = apiRepo.clone_url;
                repo.Name = apiRepo.full_name.Replace('/', '#');
                repo.Scm = ScmType.Git;
                list.Add(repo);
            }

            return list;
        }
    }
}
