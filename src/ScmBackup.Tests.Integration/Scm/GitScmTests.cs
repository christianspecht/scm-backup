using ScmBackup.Scm;
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
                string url = string.Format("https://github.com/{0}/{1}", TestHelper.EnvVar("GithubApiTests_Name"), TestHelper.EnvVar("GithubApiTests_Repo"));
                return url;
            }
        }

        internal override string PrivateRepoUrl
        {
            get { throw new NotImplementedException(); }
        }

        internal override string PublicRepoExistingCommitId
        {
            get
            {
                return TestHelper.EnvVar("GithubApiTests_Commit");
            }
        }

        internal override string PublicRepoNonExistingCommitId
        {
            get { return "00000"; }
        }
    }
}
