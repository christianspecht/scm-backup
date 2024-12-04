using GraphQL;
using GraphQL.Types.Relay.DataObjects;
using Octokit;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using ScmBackup.Configuration;
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

        //public static readonly Uri GitHubApiUrlGraphQL = new Uri("https://api.github.com/graphql");

        public GithubApi(IContext context, IScmFactory factory)
        {
            this.context = context;
            this.factory = factory;
        }

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterProject> GetProjectList( ConfigSource source )
        {
            var listProject = new List<HosterProject>();

            var productInformation = new Octokit.GraphQL.ProductHeaderValue( this.context.UserAgent, this.context.VersionNumberString );
            var connection = new Octokit.GraphQL.Connection( productInformation, source.Password );

            Boolean urlProject = true;
            string cursor = null; // The cursor keeps track of the last discussion returned
            var orderBy = new ProjectV2Order // Sorting in ascending creation date to ensure we fetch everything
                            {
                                Direction = OrderDirection.Asc, 
                                Field = ProjectV2OrderField.CreatedAt
                            };

            while ( urlProject ) {
            
                //var query = new Query().Organization( login: source.Name ).ProjectsV2( first: 100, after: "" ).Nodes.Select( project => new { project.Id, project.Title, project.ShortDescription, project.Url } ).Compile();

                var query = new Query().Organization( login: source.Name ).ProjectsV2( first: 100, after: cursor, default, default, orderBy: orderBy ).Nodes.Select( project => new { project.Id, project.Number, project.Title, project.ShortDescription, project.Url, project.Items( 100, cursor, default, default, default).PageInfo.EndCursor, project.Items( 100, cursor, default, default, default ).PageInfo.HasNextPage } ).Compile();

                var results = connection.Run( query ).Result;

                if ( results.Any() ) {

                    foreach ( var apiProject in results ) {

                        var project = new HosterProject( apiProject.Title, apiProject.Id.Value + "_" + apiProject.Number.ToString() );

                        listProject.Add( project );

                        urlProject = apiProject.HasNextPage;
                        cursor = apiProject.EndCursor;

                    }

                }

            }

            return listProject;
        }

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterRepository> GetRepositoryList(ConfigSource source, string keyProject = null)
        {
            var listRepo = new List<HosterRepository>();

            if ( !string.IsNullOrEmpty( keyProject ) ) { 
                var productInformation = new Octokit.GraphQL.ProductHeaderValue( this.context.UserAgent, this.context.VersionNumberString );
                var connection = new Octokit.GraphQL.Connection( productInformation, source.Password );

                Boolean urlRepo = true;
                string cursor = null; // The cursor keeps track of the last discussion returned
                var orderBy = new RepositoryOrder // Sorting in ascending creation date to ensure we fetch everything
                                {
                                    Direction = OrderDirection.Asc, 
                                    Field = RepositoryOrderField.CreatedAt
                                };
                int numberProject = Convert.ToInt32( keyProject.Split( "_" )[  2 ] );

                while ( urlRepo ) {

                    var query = new Query().Organization( login: source.Name ).ProjectV2( number: numberProject ).Repositories( first: 100, after: cursor, orderBy: orderBy ).Nodes.Select( repo => new { repo.Id, repo.Name, repo.Url, repo.IsPrivate, repo.HasWikiEnabled, repo.HasIssuesEnabled } ).Compile();

                    urlRepo = new Query().Organization( login: source.Name ).ProjectV2( number: numberProject ).Repositories( first: 100, after: cursor, orderBy: orderBy ).PageInfo.HasNextPage;

                    cursor = new Query().Organization( login: source.Name ).ProjectV2( number: numberProject ).Repositories( first: 100, after: cursor, orderBy: orderBy ).PageInfo.EndCursor;

                    var results = connection.Run( query ).Result;

                    var scm = this.factory.Create( ScmType.Git );

                    if ( results.Any() ) {

                        foreach ( var apiRepo in results ) {

                            var repo = new HosterRepository( apiRepo.Name, apiRepo.Name, apiRepo.Url, ScmType.Git );

                            repo.SetPrivate( repo.IsPrivate );

                            if ( apiRepo.HasWikiEnabled && apiRepo.Url.EndsWith( ".git" ) )
                            {
                                // build wiki clone URL, because API doesn't return it
                                string wikiUrl = apiRepo.Url.Substring( 0, apiRepo.Url.Length - ".git".Length ) + ".wiki.git";

                                // issue #13: the GitHub API only returns whether it's *possible* to create a wiki, but not if the repo actually *has* a wiki.
                                // So we need to skip the wiki when the URL (which we just built manually) is not a valid repository.
                                if ( scm.RemoteRepositoryExists( wikiUrl ) )
                                {
                                    repo.SetWiki( true, wikiUrl );
                                }
                            }

                            if ( apiRepo.HasIssuesEnabled )
                            {
                                // The API has only a URL for one issue (with a placeholder at the end), but this URL isn't in Octokit.
                                // So we have to build it manually:
                                repo.SetIssues( true, apiRepo.Url + "/issues/" );
                            }


                            listRepo.Add( repo );

                        }

                    }

                }
            
            }
            else {

                string className = this.GetType().Name;

                var product = new Octokit.ProductHeaderValue(this.context.UserAgent, this.context.VersionNumberString);
                var client = new GitHubClient(product);

                if (source.IsAuthenticated)
                {
                    var basicAuth = new Credentials(source.AuthName, source.Password);
                    client.Credentials = basicAuth;
                }

                IReadOnlyList<Octokit.Repository> repos = null;
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

                        listRepo.Add(repo);
                    }
                }


            }

            return listRepo;
        }

        /*
            * Modified by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        /*public List<HosterRepository> GetRepositoryList(ConfigSource source, string keyProject = null)
        {
            var list = new List<HosterRepository>();
            string className = this.GetType().Name;

            var product = new Octokit.ProductHeaderValue(this.context.UserAgent, this.context.VersionNumberString);
            var client = new GitHubClient(product);

            if (source.IsAuthenticated)
            {
                var basicAuth = new Credentials(source.AuthName, source.Password);
                client.Credentials = basicAuth;
            }

            IReadOnlyList<Octokit.Repository> repos = null;
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
        }*/
    }
}
