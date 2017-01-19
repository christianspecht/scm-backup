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

        [Fact]
        public void IsOnThisComputerReturnsTrue()
        {
            // Some of the other integration tests will need to use Git -> make sure that the machine running the tests has Git installed
            var sut = new GitScm();
            var result = sut.IsOnThisComputer(this.config);

            Assert.True(result);
        }
    }
}
