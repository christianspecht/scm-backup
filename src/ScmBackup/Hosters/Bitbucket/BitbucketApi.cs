﻿using Newtonsoft.Json;
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

            string url = "/2.0/repositories/" + source.Name;

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

                        if( cloneurl.ToLower().StartsWith( "https://" + source.AuthName.ToLower() + "@" ) )
                        {
                            cloneurl = cloneurl.ToLower().Replace( "https://" + source.AuthName.ToLower() + "@", "https://" );
                        }

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
