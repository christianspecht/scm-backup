using ScmBackup.Scm;
using System;

namespace ScmBackup.Tests.Integration.Scm
{
    public class MercurialScmTests : IScmTests
    {
        private string baseurl = "https://bitbucket.org/{0}/{1}";

        public MercurialScmTests()
        {
            this.sut = new MercurialScm(new FileSystemHelper(), new FakeContext());
        }

        internal override string PublicRepoUrl
        {
            get { return string.Format(this.baseurl, TestHelper.EnvVar("BitbucketApiTests_Name"), TestHelper.EnvVar("BitbucketApiTests_Repo")); }
        }

        internal override string PrivateRepoUrl
        {
            get { throw new NotImplementedException(); }
        }

        internal override string NonExistingRepoUrl
        {
            get { return string.Format(this.baseurl, TestHelper.EnvVar("BitbucketApiTests_Name"), "repo-does-not-exist"); }
        }

        internal override string PublicRepoExistingCommitId
        {
            get { return TestHelper.EnvVar("BitbucketApiTests_Commit"); }
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
