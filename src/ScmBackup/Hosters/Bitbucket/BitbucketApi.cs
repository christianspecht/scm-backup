using Newtonsoft.Json;
using ScmBackup.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Authentication;

namespace ScmBackup.Hosters.Bitbucket
{
    internal class BitbucketApi : IHosterApi
    {
        private readonly IHttpRequest request;

        public BitbucketApi(IHttpRequest request)
        {
            this.request = request;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource source)
        {
            var list = new List<HosterRepository>();
            string className = this.GetType().Name;

            request.SetBaseUrl("https://api.bitbucket.org");

            if (source.IsAuthenticated)
            {
                request.AddBasicAuthHeader(source.AuthName, source.Password);
            }

            string url = string.Empty;
            string apiUsername = null;

            // Issue #32: from Apr 29 2019, usernames (not team names) must be replaced by UUIDs
            if (source.Type.ToLower() == "user")
            {
                url = "/2.0/workspaces/" + source.Name;

                var result = request.Execute(url).Result;

                if (result.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<BitbucketApiUserResponse>(result.Content);
                    if (apiResponse != null)
                    {
                        apiUsername = Uri.EscapeUriString(apiResponse.uuid);
                    }
                }

                if (string.IsNullOrWhiteSpace(apiUsername))
                {
                    throw new InvalidOperationException(string.Format(Resource.ApiBitbucketCantGetUuid, source.Name));
                }
            }
            else
            {
                apiUsername = source.Name;
            }


            url = "/2.0/repositories/" + apiUsername;

            while (url != null)
            {
                var result = request.Execute(url).Result;

                if (result.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<BitbucketApiResponse>(result.Content);

                    foreach (var apiRepo in apiResponse.values)
                    {
                        ScmType type;
                        switch (apiRepo.scm.ToLower())
                        {
                            case "hg":
                                type = ScmType.Mercurial;
                                break;
                            case "git":
                                type = ScmType.Git;
                                break;
                            default:
                                throw new InvalidOperationException(string.Format(Resource.ApiInvalidScmType, apiRepo.full_name));
                        }

                        var clone = apiRepo.links.clone.Where(r => r.name == "https").First();
                        string cloneurl = clone.href;

                        var repo = new HosterRepository(apiRepo.full_name, apiRepo.slug, cloneurl, type);

                        repo.SetPrivate(apiRepo.is_private);

                        if (apiRepo.has_wiki)
                        {
                            string wikiUrl = cloneurl + "/wiki";
                            repo.SetWiki(true, wikiUrl.ToString());
                        }

                        // TODO: Issues

                        list.Add(repo);
                    }

                    url = apiResponse.next;
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
            }

            return list;
        }
    }
}
