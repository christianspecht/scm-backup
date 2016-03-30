using System.Collections.Generic;
using Xunit;

namespace ScmBackup.Tests
{
    public class ValidatingConfigReaderTests
    {
        private readonly IConfigReader sut;
        private readonly FakeConfigReader reader;
        private readonly FakeLogger logger;

        public ValidatingConfigReaderTests()
        {
            reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            logger = new FakeLogger();

            sut = new ValidatingConfigReader(reader, logger);
        }

        [Fact]
        public void DoesntLogErrorWhenLocalFolderIsSet()
        {
            sut.ReadConfig();

            Assert.False(logger.LoggedSomething);
        }

        [Fact]
        public void LogsErrorWhenLocalFolderIsMissing()
        {
            reader.FakeConfig.LocalFolder = null;

            sut.ReadConfig();

            Assert.True(logger.LoggedSomething);
            Assert.Equal(ErrorLevel.Error, logger.LastErrorLevel);
        }

        [Fact]
        public void ReturnsNullWhenLocalFolderIsMissing()
        {
            reader.FakeConfig.LocalFolder = null;

            var result = sut.ReadConfig();

            Assert.Null(result);
        }

        [Fact]
        public void DoesntErrorWhenWaitSecondsIsMissing()
        {
            reader.FakeConfig.WaitSecondsOnError = 0;

            var result = sut.ReadConfig();

            Assert.False(logger.LoggedSomething);
        }

        [Fact]
        public void LogsErrorAndReturnsNullWhenThereAreNoConfigSources()
        {
            reader.FakeConfig.Sources = new List<ConfigSource>();

            var result = sut.ReadConfig();

            Assert.True(logger.LoggedSomething);
            Assert.Equal(ErrorLevel.Error, logger.LastErrorLevel);
            Assert.Null(result);
        }


        [Fact]
        public void DoesntErrorWhenThereIsOneConfigSource()
        {
            var result = sut.ReadConfig();

            Assert.False(logger.LoggedSomething);
            Assert.NotNull(result);
        }
    }
}
