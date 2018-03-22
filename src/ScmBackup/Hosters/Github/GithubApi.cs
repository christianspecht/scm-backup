using Octokit;
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
        private readonly ILogger logger;

        public GithubApi(ILogger logger)
        {
            this.logger = logger;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource source)
        {
            var list = new List<HosterRepository>();
            string className = this.GetType().Name;

            var product = new ProductHeaderValue("SCM_Backup");
            var client = new GitHubClient(product);

            bool isAuthenticated = !String.IsNullOrWhiteSpace(source.AuthName) && !String.IsNullOrWhiteSpace(source.Password);
            if (isAuthenticated)
            {
                var basicAuth = new Credentials(source.AuthName, source.Password);
                client.Credentials = basicAuth;
            }

            IReadOnlyList<Repository> repos = null;
            try
            {
                switch (source.Type.ToLower())
                {
                    case "user":

                        if (isAuthenticated)
                        {
                            repos = client.Repository.GetAllForCurrent().Result;
                        }
                        else
                        {
                            repos = client.Repository.GetAllForUser(source.Name).Result;
                        }
                        break;

                    case "org":

                        repos = client.Repository.GetAllForOrg(source.Name).Result;
                        break;
                }
            }
            catch (ApiException e)
            {
                switch (e.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new AuthenticationException(string.Format(Resource.ApiAuthenticationFailed, source.AuthName));
                    case HttpStatusCode.Forbidden:
                        throw new SecurityException(Resource.ApiMissingPermissions);
                    case HttpStatusCode.NotFound:
                        throw new InvalidOperationException(string.Format(Resource.ApiInvalidUsername, source.Title));
                }
            }

            this.logger.Log(ErrorLevel.Info, Resource.ApiGettingUrl, className, source.Title);

            if (repos != null)
            {
                foreach(var apiRepo in repos)
                {
                    var repo = new HosterRepository(apiRepo.FullName, apiRepo.CloneUrl, ScmType.Git);

                    if (apiRepo.HasWiki && apiRepo.CloneUrl.EndsWith(".git"))
                    {
                        // build wiki clone URL, because API doesn't return it
                        string wikiUrl = apiRepo.CloneUrl.Substring(0, apiRepo.CloneUrl.Length - ".git".Length) + ".wiki.git";

                        repo.SetWiki(true, wikiUrl);
                    }

                    if (apiRepo.HasIssues)
                    {
                        // The API has only a URL for one issue (with a placeholder at the end), but this URL isn't in Octokit.
                        // So we have to build it manually:
                        repo.SetIssues(true, apiRepo.Url + "/issues/");
                    }

                    list.Add(repo);
                }
            }

            return list;
        }
    }
}
