using Newtonsoft.Json;
using ScmBackup.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Security.Authentication;

namespace ScmBackup.Hosters.Github
{
    /// <summary>
    /// Calls the GitHub API
    /// </summary>
    internal class GithubApi : IHosterApi
    {
        private readonly IHttpRequest request;
        private readonly ILogger logger;

        public GithubApi(IHttpRequest request, ILogger logger)
        {
            this.request = request;
            this.logger = logger;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource source)
        {
            var list = new List<HosterRepository>();
            string className = this.GetType().Name;

            // https://developer.github.com/v3/#schema
            request.SetBaseUrl("https://api.github.com");

            // https://developer.github.com/v3/#current-version
            request.AddHeader("Accept", "application/vnd.github.v3+json");

            // https://developer.github.com/v3/#user-agent-required
            request.AddHeader("User-Agent", Resource.AppTitle);
            
            bool isAuthenticated = !String.IsNullOrWhiteSpace(source.AuthName) && !String.IsNullOrWhiteSpace(source.Password);
            if (isAuthenticated)
            {
                // https://developer.github.com/v3/auth/#basic-authentication
                request.AddBasicAuthHeader(source.AuthName, source.Password);
            }

            string url = string.Empty;
            switch (source.Type.ToLower())
            {
                case "user":

                    if (isAuthenticated)
                    {
                        // https://developer.github.com/v3/repos/#list-your-repositories
                        url = "/user/repos";
                    }
                    else
                    {
                        // https://developer.github.com/v3/repos/#list-user-repositories
                        url = string.Format("/users/{0}/repos", source.Name);
                    }
                    break;

                case "org":

                    // https://developer.github.com/v3/repos/#list-organization-repositories
                    url = string.Format("/orgs/{0}/repos", source.Name);
                    break;
            }

            this.logger.Log(ErrorLevel.Info, Resource.ApiGettingUrl, className, request.HttpClient.BaseAddress.ToString() + url);
            var result = request.Execute(url).Result;

            if (result.IsSuccessStatusCode)
            {
                var apiResponse = JsonConvert.DeserializeObject<List<GithubApiResponse>>(result.Content);
                foreach (var apiRepo in apiResponse)
                {
                    var repo = new HosterRepository(apiRepo.full_name, apiRepo.clone_url, ScmType.Git);

                    if (apiRepo.has_wiki && apiRepo.clone_url.EndsWith(".git"))
                    {
                        // build wiki clone URL, because API doesn't return it
                        string wikiUrl = apiRepo.clone_url.Substring(0, apiRepo.clone_url.Length - ".git".Length) + ".wiki.git";

                        repo.SetWiki(true, wikiUrl);
                    }

                    if (apiRepo.has_issues)
                    {
                        // GitHub has no clone URL for the issues, it's only possible to get them in JSON format via the API.
                        // The API has only a URL for one issue (with a placeholder at the end, so we need to remove the placeholder).
                        repo.SetIssues(true, apiRepo.issues_url.Replace("{/number}", ""));
                    }

                    list.Add(repo);
                }
            }
            else
            {
                switch (result.Status)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new AuthenticationException(string.Format(Resource.ApiAuthenticationFailed, source.AuthName));
                    case HttpStatusCode.Forbidden:
                        throw new SecurityException(Resource.ApiMissingPermissions);
                    case HttpStatusCode.NotFound:
                        throw new InvalidOperationException(string.Format(Resource.ApiInvalidUsername, source.Name));
                }
            }

            return list;
        }
    }
}
