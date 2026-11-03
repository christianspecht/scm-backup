using ScmBackup.Configuration;
using ScmBackup.Hosters;
using ScmBackup.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public abstract class IHosterApiTests
    {
        // user, repo etc. (must be implemented in the child classes)

        /// <summary>
        /// test user
        /// </summary>
        internal abstract string HosterUser { get; }

        /// <summary>
        /// test organization
        /// </summary>
        internal abstract string HosterOrganization { get; }

        /// <summary>
        /// a repository with wiki (must exist under user AND organization)
        /// </summary>
        internal abstract string HosterRepo { get; }

        /// <summary>
        /// a commit in the repository
        /// </summary>
        internal abstract string HosterCommit { get; }

        /// <summary>
        /// a commit in the wiki
        /// </summary>
        internal abstract string HosterWikiCommit { get; }

        /// <summary>
        /// a user with enough public repos to test pagination
        /// </summary>
        internal abstract string HosterPaginationUser { get; }

        /// <summary>
        /// a private repository, must exist under the user from the environment variable. Return null to skip tests
        /// </summary>
        internal abstract string HosterPrivateRepo { get; }

        /// <summary>
        /// environment variables with this prefix (from environment-variables.ps1) are used 
        /// </summary>
        internal abstract string EnvVarPrefix { get; }

        /// <summary>
        /// "hoster" value for config sources 
        /// </summary>
        internal abstract string ConfigHoster { get; }

        /// <summary>
        /// minimum number of repos which must be returned in "GetRepositoryList_PaginationWorks" -> should be more than the default page size of the hoster's API
        /// </summary>
        internal abstract int Pagination_MinNumberOfRepos { get; }

        /// <summary>
        /// set this to true in order to skip all tests without authentication (because of rate limits, see #7) 
        /// </summary>
        internal abstract bool SkipUnauthenticatedTests { get; }

        /// <summary>
        /// this needs to be created in the child classes' constructor:
        /// </summary>
        internal IHosterApi sut;

        /// <summary>
        /// skip certain tests because of https://github.com/christianspecht/scm-backup/issues/15<para/>
        /// Child classes which need to skip those tests need to implement this and return true
        /// </summary>
        protected virtual bool SkipTestsIssue15()
        {
            return false;
        }

        [Fact]
        public void SutWasSetInChildClass()
        {
            Assert.NotNull(this.sut);
        }

        [SkippableFact]
        public void GetRepositoryList_UnauthenticatedUser_Executes()
        {
            Skip.If(this.SkipUnauthenticatedTests);

            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "user";
            source.Name = this.HosterUser;

            var repoList = this.sut.GetRepositoryList(source);

            // at least one result?
            Assert.NotNull(repoList);
            Assert.True(repoList.Count > 0);

            // specific repo exists?
            string expectedName = TestHelper.BuildRepositoryName(source.Name, this.HosterRepo);
            var repo = repoList.Where(r => r.FullName == expectedName).FirstOrDefault();
            Assert.NotNull(repo);
            Assert.True(ValidateUrls(repo));
        }


        [SkippableFact]
        public void GetRepositoryList_NonExistingUser_ThrowsException()
        {
            Skip.If(this.SkipUnauthenticatedTests);

            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "user";
            source.Name = "scm-backup-testuser-does-not-exist";

            List<HosterRepository> repoList;
            Assert.ThrowsAny<Exception>(() => repoList = sut.GetRepositoryList(source));
        }

        [Fact]
        public void GetRepositoryList_AuthenticatedUser_InvalidPasswordThrowsException()
        {
            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "user";
            source.Name = this.HosterUser;
            source.AuthName = TestHelper.EnvVar(this.EnvVarPrefix, "Name");
            source.Password = "invalid-password";

            List<HosterRepository> repoList;
            Assert.ThrowsAny<Exception>(() => repoList = sut.GetRepositoryList(source));
        }

        [SkippableFact]
        public void GetRepositoryList_AuthenticatedUser_Executes()
        {
            Skip.If(this.SkipTestsIssue15());

            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "user";
            source.Name = this.HosterUser;
            source.AuthName = TestHelper.EnvVar(this.EnvVarPrefix, "Name");
            source.Password = TestHelper.EnvVar(this.EnvVarPrefix, "PW");

            var repoList = sut.GetRepositoryList(source);

            // at least one result?
            Assert.NotNull(repoList);
            Assert.True(repoList.Count > 0);

            // specific repo exists?
            string expectedName = TestHelper.BuildRepositoryName(source.Name, this.HosterRepo);
            var repo = repoList.Where(r => r.FullName == expectedName).FirstOrDefault();
            Assert.NotNull(repo);
            Assert.True(ValidateUrls(repo));
        }

        [SkippableFact]
        public void GetRepositoryList_UnauthenticatedOrganization_Executes()
        {
            Skip.If(this.SkipUnauthenticatedTests);

            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "org";
            source.Name = this.HosterOrganization;

            var repoList = sut.GetRepositoryList(source);

            // at least one result?
            Assert.NotNull(repoList);
            Assert.True(repoList.Count > 0);

            // specific repo exists?
            string expectedName = TestHelper.BuildRepositoryName(source.Name, this.HosterRepo);
            var repo = repoList.Where(r => r.FullName == expectedName).FirstOrDefault();
            Assert.NotNull(repo);
            Assert.True(ValidateUrls(repo));
        }

        [SkippableFact]
        public void GetRepositoryList_NonExistingOrganization_ThrowsException()
        {
            Skip.If(this.SkipUnauthenticatedTests);

            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "org";
            source.Name = "scm-backup-testorg-does-not-exist";

            List<HosterRepository> repoList;
            Assert.ThrowsAny<Exception>(() => repoList = sut.GetRepositoryList(source));
        }

        [Fact]
        public void GetRepositoryList_AuthenticatedOrganization_Executes()
        {
            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "org";
            source.Name = this.HosterOrganization;
            source.AuthName = TestHelper.EnvVar(this.EnvVarPrefix, "Name");
            source.Password = TestHelper.EnvVar(this.EnvVarPrefix, "PW");

            var repoList = sut.GetRepositoryList(source);

            // at least one result?
            Assert.NotNull(repoList);
            Assert.True(repoList.Count > 0);

            // specific repo exists?
            string expectedName = TestHelper.BuildRepositoryName(source.Name, this.HosterRepo);
            var repo = repoList.Where(r => r.FullName == expectedName).FirstOrDefault();
            Assert.NotNull(repo);
            Assert.True(ValidateUrls(repo));
        }

        [SkippableFact]
        public void GetRepositoryList_PrivateRepoIsMarkedAsPrivate()
        {
            string repoName = this.HosterPrivateRepo;
            Skip.If(repoName == null, "There's no private repo for this hoster");

            Skip.If(this.SkipTestsIssue15());

            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "user";
            source.Name = TestHelper.EnvVar(this.EnvVarPrefix, "Name");
            source.AuthName = source.Name;
            source.Password = TestHelper.EnvVar(this.EnvVarPrefix, "PW");

            var repoList = sut.GetRepositoryList(source);

            // specific repo exists and is private?
            string expectedName = TestHelper.BuildRepositoryName(source.Name, repoName);
            var repo = repoList.Where(r => r.FullName == expectedName).FirstOrDefault();
            Assert.NotNull(repo);
            Assert.True(repo.IsPrivate);
        }

        [SkippableFact]
        public void GetRepositoryList_PaginationWorks()
        {
            // Name and AuthName must be equal for all of the currently supported hosters, so this test must be without authentication
            // TODO: make authentication for this test configurable when a new hoster without this limitation is added
            Skip.If(this.SkipUnauthenticatedTests);

            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "user";
            source.Name = this.HosterPaginationUser;

            var repoList = sut.GetRepositoryList(source);

            Assert.True(repoList.Count > this.Pagination_MinNumberOfRepos);
        }

        private bool ValidateUrls(HosterRepository repo)
        {
            bool result = true;

            var validator = new UrlHelper();
            if (!validator.UrlIsValid(repo.CloneUrl))
            {
                return false;
            }

            if (repo.HasWiki)
            {
                if (!validator.UrlIsValid(repo.WikiUrl))
                {
                    return false;
                }
            }

            if (repo.HasIssues)
            {
                if (!validator.UrlIsValid(repo.IssueUrl))
                {
                    return false;
                }
            }

            return result;
        }
    }
}
