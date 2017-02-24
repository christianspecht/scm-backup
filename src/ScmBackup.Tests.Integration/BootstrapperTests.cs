using ScmBackup.CompositionRoot;
using ScmBackup.Hosters.Bitbucket;
using ScmBackup.Hosters.Github;
using System;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class BootstrapperTests
    {
        /// <summary>
        /// Just initialize the container like on startup.
        /// If a dependency could not be resolved, it will throw an exception.
        /// </summary>
        [Fact]
        public void DependenciesWereResolved()
        {
            bool error = false;

            try
            {
                var container = Bootstrapper.BuildContainer();   
            }
            catch (InvalidOperationException)
            {
                error = true;
            }

            Assert.False(error);
        }

        /// <summary>
        /// Make sure the name-based convention for the hosters and their subclasses works correctly:
        /// Resolve from container and check whether *Github*Hoster actually contains *Github*Api, and so on.
        /// </summary>
        [Fact]
        public void HosterAutoRegistrationConventionWorks()
        {
            var container = Bootstrapper.BuildContainer();

            var gh = container.GetInstance<GithubHoster>();
            Assert.Equal(typeof(GithubApi), gh.Api.GetType());
            Assert.Equal(typeof(GithubConfigSourceValidator), gh.Validator.GetType());

            var bb = container.GetInstance<BitbucketHoster>();
            Assert.Equal(typeof(BitbucketApi), bb.Api.GetType());
            Assert.Equal(typeof(BitbucketConfigSourceValidator), bb.Validator.GetType());
        }
    }
}
