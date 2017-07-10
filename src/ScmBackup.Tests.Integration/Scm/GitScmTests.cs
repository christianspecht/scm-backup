using ScmBackup.Scm;
using System;

namespace ScmBackup.Tests.Integration.Scm
{
    public class GitScmTests : IScmTests
    {
        public GitScmTests()
        {
            this.sut = new GitScm();
        }

        internal override string PublicRepoUrl
        {
            get { return "https://github.com/scm-backup-testuser/scm-backup"; }
        }

        internal override string PrivateRepoUrl
        {
            get { throw new NotImplementedException(); }
        }
    }
}
