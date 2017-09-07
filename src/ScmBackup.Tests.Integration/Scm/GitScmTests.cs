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

        internal override string PublicRepoExistingCommitId
        {
            get { return "3a91f6409f0cef7a3bd2c80cad389fa844b41e3c"; }
        }

        internal override string PublicRepoNonExistingCommitId
        {
            get { return "00000"; }
        }
    }
}
