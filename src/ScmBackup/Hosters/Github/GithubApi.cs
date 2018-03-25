using Octokit;
using System;
using System.Collections.Generic;

namespace ScmBackup.Hosters.Github
{
    /// <summary>
    /// Calls the GitHub API
    /// </summary>
    internal class GithubApi : IHosterApi
    {
        private readonly IContext context;

        public GithubApi(IContext context)
        {
            this.context = context;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource source)
        {
            var list = new List<HosterRepository>();
            string className = this.GetType().Name;

            var product = new ProductHeaderValue(this.context.UserAgent, this.context.VersionNumberString);
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

                        repos = client.Repository.GetAllForUser(source.Name).Result;
                        break;

                    case "org":

                        repos = client.Repository.GetAllForOrg(source.Name).Result;
                        break;
                }
            }
            catch (Exception e)
            {
                string message = e.Message;

                if (e.InnerException is AuthorizationException)
                {
                    message = string.Format(Resource.ApiAuthenticationFailed, source.AuthName);
                }
                else if (e.InnerException is ForbiddenException)
                {
                    message = Resource.ApiMissingPermissions;
                }
                else if (e.InnerException is NotFoundException)
                {
                    message = string.Format(Resource.ApiInvalidUsername, source.Name);
                }

                throw new ApiException(message, e);
            }

            if (repos != null)
            {
                foreach(var apiRepo in repos)
                {
                    var repo = new HosterRepository(apiRepo.FullName, apiRepo.Name, apiRepo.CloneUrl, ScmType.Git);

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
