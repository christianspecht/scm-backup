using ScmBackup.Scm;
using ScmBackup.Tests.Hosters;
using System;

namespace ScmBackup.Tests.Integration.Scm
{
    public class GitScmTests : IScmTests
    {
        public GitScmTests()
        {
            this.sut = new GitScm(new FileSystemHelper(), new FakeContext());
        }

        internal override string PublicRepoUrl
        {
            get
            {
                string url = CloneUrlBuilder.GithubCloneUrl(TestHelper.EnvVar("Github_Name"), TestHelper.EnvVar("Github_Repo"));
                return url;
            }
        }

        internal override string PrivateRepoUrl
        {
            get { throw new NotImplementedException(); }
        }

        internal override string NonExistingRepoUrl
        {
            get { return CloneUrlBuilder.GithubCloneUrl(TestHelper.EnvVar("Github_Name"), "repo-does-not-exist"); }
        }

        internal override string PublicRepoExistingCommitId
        {
            get
            {
                return TestHelper.EnvVar("Github_Commit");
            }
        }

        internal override string PublicRepoNonExistingCommitId
        {
            get { return "00000"; }
        }
    }
}
