﻿using Newtonsoft.Json;
using ScmBackup.Configuration;
using ScmBackup.Http;
using ScmBackup.Scm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace ScmBackup.Hosters.Gitlab
{
    internal class GitlabApi : IHosterApi
    {
        private readonly IHttpRequest req;
        private readonly IScmFactory factory;

        public GitlabApi(IHttpRequest req, IScmFactory factory)
        {
            this.req = req;
            this.factory = factory;
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

            var scm = this.factory.Create(ScmType.Git);

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
                        string cloneUrl = apiRepo.http_url_to_repo;

                        var repo = new HosterRepository(apiRepo.path_with_namespace, apiRepo.name, cloneUrl, ScmType.Git);

                        repo.SetPrivate(apiRepo.visibility == "private");

                        // wiki: the API only returns if it's enabled, but not if it actually contains pages
                        // The Git repo always exists, even when the wiki has no pages.
                        // So we can't check for the existence of the Git repo -> we need to make another API call to check if it has at least one page
                        if (apiRepo.wiki_enabled && cloneUrl.EndsWith(".git"))
                        {
                            // Rate limit of 10 requests per second per IP address (https://docs.gitlab.com/ee/user/gitlab_com/index.html#gitlabcom-specific-rate-limits)
                            // --> block long enough so that 10 requests will take longer than a second
                            Task.Delay(110).Wait();

                            string wikiUrl = string.Format("/api/v4/projects/{0}/wikis", apiRepo.id);
                            var wikiResult = req.Execute(wikiUrl).Result;
                            if (wikiResult.IsSuccessStatusCode)
                            {
                                var wikis = JsonConvert.DeserializeObject<List<GitlabApiWiki>>(wikiResult.Content);
                                if (wikis.Any())
                                {
                                    repo.SetWiki(true, cloneUrl.Substring(0, cloneUrl.Length - ".git".Length) + ".wiki.git");
                                }
                            }
                        }

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
