using ScmBackup.Scm;
using System;

namespace ScmBackup.Tests.Integration.Scm
{
    public class GitScmTests : IScmTests
    {
        private string baseurl = "https://github.com/{0}/{1}";

        public GitScmTests()
        {
            this.sut = new GitScm(new FileSystemHelper(), new FakeContext());
        }

        internal override string PublicRepoUrl
        {
            get
            {
                string url = string.Format(this.baseurl, TestHelper.EnvVar("GithubApiTests_Name"), TestHelper.EnvVar("GithubApiTests_Repo"));
                return url;
            }
        }

        internal override string PrivateRepoUrl
        {
            get { throw new NotImplementedException(); }
        }

        internal override string NonExistingRepoUrl
        {
            get { return string.Format(this.baseurl, TestHelper.EnvVar("GithubApiTests_Name"), "repo-does-not-exist"); }
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
