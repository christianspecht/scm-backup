using ScmBackup.Scm;

namespace ScmBackup.Tests.Integration.Scm
{
    public class GitScm_Tests : IScmTests
    {
        public GitScm_Tests()
        {
            this.sut = new GitScm();
        }
    }
}
