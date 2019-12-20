using Newtonsoft.Json;
using ScmBackup.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Authentication;

namespace ScmBackup.Hosters.Gitlab
{
    internal class GitlabApi : IHosterApi
    {
        private readonly IHttpRequest req;

        public GitlabApi(IHttpRequest req)
        {
            this.req = req;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource config)
        {
            var repos = new List<HosterRepository>();

            this.req.SetBaseUrl("https://gitlab.com");
            this.req.AddHeader("Accept", "application/json");

            if (config.IsAuthenticated)
            {
                this.req.AddHeader("Private-Token", config.Password);
            }

            string type = "users";
            string args = string.Empty;

            if (config.Type.ToLower() != "user")
            {
                type = "groups";
                args = "?include_subgroups=true";
            }

            string url = string.Format("/api/v4/{0}/{1}/projects{2}", type, config.Name, args);

            while (url != null)
            {
                var result = req.Execute(url).Result;
                url = null;
                if (result.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<List<GitlabApiRepo>>(result.Content);

                    foreach (var apiRepo in response)
                    {
                        var repo = new HosterRepository(apiRepo.path_with_namespace, apiRepo.name, apiRepo.http_url_to_repo, ScmType.Git);

                        repo.SetPrivate(apiRepo.visibility == "private");

                        // wiki_enabled

                        // TODO: Issues
                        // _links -> issues ??
                        // issues_access_level ??
                        
                        repos.Add(repo);
                    }
                    
                    if (result.Headers.Contains("Link"))
                    {
                        // There are multiple links, but all in one header value
                        // https://docs.gitlab.com/ee/api/README.html#pagination-link-header
                        string links = result.Headers.GetValues("Link").First();

                        // The API returns something like this and we need the link named "next":
                        // <https://gitlab.com/api/foo>; rel="next", <https://gitlab.com/api/bar>; rel="first", <https://gitlab.com/api/baz>; rel="last"
                        foreach (var link in links.Split(','))
                        {
                            var items = link.Split(';');
                            if (items[1].Contains("next"))
                            {
                                url = items[0].Trim('<', '>', ' ');
                                break;
                            }
                        }
                    }
                }
                else
                {
                    switch (result.Status)
                    {
                        case HttpStatusCode.Unauthorized:
                            throw new AuthenticationException(string.Format(Resource.ApiAuthenticationFailed, config.AuthName));
                        case HttpStatusCode.Forbidden:
                            throw new SecurityException(Resource.ApiMissingPermissions);
                        case HttpStatusCode.NotFound:
                            throw new InvalidOperationException(string.Format(Resource.ApiInvalidUsername, config.Name));
                    }
                }
            }

            return repos;
        }
    }
}
