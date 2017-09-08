using ScmBackup.Scm;
using Xunit;

namespace ScmBackup.Tests.Scm
{
    public class CommandLineResultTests
    {
        [Fact]
        public void OutputReturnsStandard()
        {
            var sut = new CommandLineResult();
            sut.StandardOutput = "foo";

            Assert.Equal(sut.StandardOutput, sut.Output);
        }

        [Fact]
        public void OutputReturnsError()
        {
            var sut = new CommandLineResult();
            sut.StandardError = "foo";

            Assert.Equal(sut.StandardError, sut.Output);
        }

        [Fact]
        public void NewInstanceReturnsNotSuccessful()
        {
            var sut = new CommandLineResult();
            Assert.False(sut.Successful);
        }

        [Theory]
        [InlineData(true, 0)]
        [InlineData(false, 1)]
        public void SuccessfulReturnsCorrectValue(bool expected, int exitcode)
        {
            var sut = new CommandLineResult();
            sut.ExitCode = exitcode;

            Assert.Equal(expected, sut.Successful);
        }
    }
}
