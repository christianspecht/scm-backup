using ScmBackup.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

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

            string url = string.Empty;
            if (config.Type.ToLower() == "user")
            {
                url = "/api/v4/users/" + config.Name + "/projects";
            }
            else
            {
                throw new NotImplementedException();
            }

            var result = req.Execute(url).Result;
            if (result.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<List<GitlabApiRepo>>(result.Content);

                foreach (var apiRepo in response)
                {
                    var repo = new HosterRepository(apiRepo.path_with_namespace, apiRepo.name, apiRepo.http_url_to_repo, ScmType.Git);

                    repo.SetPrivate(apiRepo.visibility == "private");

                    repos.Add(repo);
                }
            }
            else
            {
                System.Diagnostics.Debugger.Break();
            }

            return repos;
        }
    }
}
