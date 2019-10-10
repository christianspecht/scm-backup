using Octokit;
using ScmBackup.Scm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace ScmBackup.Hosters.Github
{
    /// <summary>
    /// Calls the GitHub API
    /// </summary>
    internal class GithubApi : IHosterApi
    {
        private readonly IContext context;
        private readonly IScmFactory factory;

        public GithubApi(IContext context, IScmFactory factory)
        {
            this.context = context;
            this.factory = factory;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource source)
        {
            var list = new List<HosterRepository>();
            string className = this.GetType().Name;

            var product = new ProductHeaderValue(this.context.UserAgent, this.context.VersionNumberString);
            var client = new GitHubClient(product);

            if (source.IsAuthenticated)
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

                        if (source.IsAuthenticated)
                        {
                            // If authenticated, lists ALL repos for the user, include private ones (https://developer.github.com/v3/repos/#list-your-repositories)
                            repos = client.Repository.GetAllForCurrent().Result;
                        }
                        else
                        {
                            // GetAllForCurrent REQUIRES authentication, so we must use GetAllForUser when not authenticated to list public repos (https://developer.github.com/v3/repos/#list-user-repositories)
                            repos = client.Repository.GetAllForUser(source.Name).Result;
                        }

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

            // Check the right scope: 
            // #29: when authenticated, the personal access token must at least have the "repo" scope, otherwise the API doesn't return private repos
            // There are no tests for this, because this would require everybody to create a second token with insufficient permissions in order to run the integration tests.
            // -> so we just throw an exception here, which means that most of the integration tests will fail
            if (source.IsAuthenticated)
            {
                var info = client.GetLastApiInfo();
                if (!info.OauthScopes.Contains("repo"))
                {
                    throw new SecurityException(string.Format(Resource.ApiGithubNotEnoughScope, source.Title));
                }

            }

            var scm = this.factory.Create(ScmType.Git);

            if (repos != null)
            {
                // #29: GetAllForCurrent (see above) returns ALL repos the user has access to (for example, the repos of orgs where the user is a member).
                // We only want the repos under the user:
                var userRepos = repos.Where(r => r.Owner.Login == source.Name);

                foreach (var apiRepo in userRepos)
                {
                    var repo = new HosterRepository(apiRepo.FullName, apiRepo.Name, apiRepo.CloneUrl, ScmType.Git);

                    repo.SetPrivate(apiRepo.Private);

                    if (apiRepo.HasWiki && apiRepo.CloneUrl.EndsWith(".git"))
                    {
                        // build wiki clone URL, because API doesn't return it
                        string wikiUrl = apiRepo.CloneUrl.Substring(0, apiRepo.CloneUrl.Length - ".git".Length) + ".wiki.git";

                        // issue #13: the GitHub API only returns whether it's *possible* to create a wiki, but not if the repo actually *has* a wiki.
                        // So we need to skip the wiki when the URL (which we just built manually) is not a valid repository.
                        if (scm.RemoteRepositoryExists(wikiUrl))
                        {
                            repo.SetWiki(true, wikiUrl);
                        }
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
