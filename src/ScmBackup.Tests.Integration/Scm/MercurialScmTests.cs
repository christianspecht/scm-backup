using ScmBackup.Scm;
using ScmBackup.Tests.Hosters;
using System;

namespace ScmBackup.Tests.Integration.Scm
{
    public class MercurialScmTests : IScmTests
    {
        public MercurialScmTests()
        {
            this.sut = new MercurialScm(new FileSystemHelper(), new FakeContext());
        }

        internal override string PublicRepoUrl
        {
            get { return CloneUrlBuilder.BitbucketCloneUrl(TestHelper.EnvVar("Bitbucket_Name"), TestHelper.EnvVar("Bitbucket_Repo")); }
        }

        internal override string PrivateRepoUrl
        {
            get { throw new NotImplementedException(); }
        }

        internal override string NonExistingRepoUrl
        {
            get { return CloneUrlBuilder.BitbucketCloneUrl(TestHelper.EnvVar("Bitbucket_Name"), "repo-does-not-exist"); }
        }

        internal override string PublicRepoExistingCommitId
        {
            get { return TestHelper.EnvVar("Bitbucket_Commit"); }
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
