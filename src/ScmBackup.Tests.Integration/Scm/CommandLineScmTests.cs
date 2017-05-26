using System;
using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration.Scm
{
    public class CommandLineScmTests
    {
        [Fact]
        public void ThrowsExceptionWhenIsOnThisComputerWasNotCalledBefore()
        {
            var sut = new FakeCommandLineScm();
            Assert.Throws<InvalidOperationException>(() => sut.ExecuteCommandDirectly());
        }

        [Fact]
        public void ReallyExecutes()
        {
            var config = new Config();

            var sut = new FakeCommandLineScm();
            var result = sut.IsOnThisComputer(config);

            Assert.True(result);
        }

        [Fact]
        public void ThrowsWhenPathFromConfigDoesntExist()
        {
            var sut = new FakeCommandLineScm();

            var config = new Config();
            config.Scms.Add(new ConfigScm { Name = sut.ShortName, Path = sut.FakeCommandName });

            Assert.Throws<FileNotFoundException>(() => sut.IsOnThisComputer(config));
        }

        [Fact(Skip = "TODO: find out the actual path to the command")]
        public void ReallyExecutesWithPathFromConfig()
        {
            var sut = new FakeCommandLineScm();

            var config = new Config();
            config.Scms.Add(new ConfigScm { Name = sut.ShortName, Path = sut.FakeCommandName });

            var result = sut.IsOnThisComputer(config);

            Assert.True(result);
        }
    }
}
