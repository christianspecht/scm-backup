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
            var sut = new FakeCommandLineScm();
            var result = sut.IsOnThisComputer();

            Assert.True(result);
        }

        [Fact]
        public void ThrowsWhenPathFromConfigDoesntExist()
        {
            var sut = new FakeCommandLineScm();

            var config = new Config();
            config.Scms.Add(new ConfigScm { Name = sut.ShortName, Path = sut.FakeCommandNameNotExisting });

            sut.Context = FakeContext.BuildFakeContextWithConfig(config);

            Assert.Throws<FileNotFoundException>(() => sut.IsOnThisComputer());
        }

        [Fact]
        public void ReallyExecutesWithPathFromConfig()
        {
            var sut = new FakeCommandLineScm();

            var config = new Config();
            config.Scms.Add(new ConfigScm { Name = sut.ShortName, Path = sut.FakeCommandName });

            sut.Context = FakeContext.BuildFakeContextWithConfig(config);

            var result = sut.IsOnThisComputer();

            Assert.True(result);
        }

        [Fact]
        public void ExecuteReturnsOutput()
        {
            var sut = new FakeCommandLineScm();
            sut.IsOnThisComputer();

            var result = sut.ExecuteCommandDirectly();

            Assert.Equal(sut.FakeCommandResult, result);
        }
    }
}
