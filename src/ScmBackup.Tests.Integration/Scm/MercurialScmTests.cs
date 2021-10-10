using ScmBackup.Http;
using ScmBackup.Scm;
using ScmBackup.Tests.Hosters;
using System;

namespace ScmBackup.Tests.Integration.Scm
{
    // Test class disabled (=private) because none of the currently supported hosters supports Mercurial anymore.
    // If a Mercurial hoster is supported in the future, we need new test repos
    class MercurialScmTests : IScmTests
    {
        public MercurialScmTests()
        {
            this.sut = new MercurialScm(new FileSystemHelper(), new FakeContext(), new UrlHelper());
        }

        internal override string PublicRepoUrl
        {
            get { return CloneUrlBuilder.BitbucketCloneUrl("scm-backup-testuser", "scm-backup-test"); }
        }

        internal override string PrivateRepoUrl
        {
            get { return CloneUrlBuilder.BitbucketCloneUrl(TestHelper.EnvVar("Bitbucket_Name"), TestHelper.EnvVar("Bitbucket_RepoPrivate")); }
        }

        internal override ScmCredentials PrivateRepoCredentials
        {
            get { return new ScmCredentials(TestHelper.EnvVar("Bitbucket_Name"), TestHelper.EnvVar("Bitbucket_PW")); }
        }

        internal override string NonExistingRepoUrl
        {
            get { return CloneUrlBuilder.BitbucketCloneUrl("scm-backup-testuser", "repo-does-not-exist"); }
        }

        internal override string DotRepoUrl
        {
            get { return null; }
        }

        internal override string PublicRepoExistingCommitId
        {
            get { return "617f9e55262be7b6d1c9db081ec351ff25c9a0e5"; }
        }

        internal override string PublicRepoNonExistingCommitId
        {
            get
            {
                // note: in Mercurial, commit id "000000000000" is valid, so we need to check for something which doesn't contain "000..."
                return "1111111111";
            }
        }
    }
}
