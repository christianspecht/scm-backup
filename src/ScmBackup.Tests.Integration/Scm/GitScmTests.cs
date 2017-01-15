using ScmBackup.Scm;
using Xunit;

namespace ScmBackup.Tests.Integration.Scm
{
    public class GitScmTests
    {
        public GitScmTests()
        {
            this.config = new Config();
        }

        private readonly Config config;

        [Fact]
        public void IsOnThisComputerExecutes()
        {
            var sut = new GitScm();
            sut.IsOnThisComputer(this.config);
        }
    }
}
