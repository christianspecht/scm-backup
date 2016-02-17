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
            logger = new FakeLogger();

            var config = new Config();
            config.LocalFolder = "foo";
            config.WaitSecondsOnError = 1;
            reader.FakeConfig = config;

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
            Assert.Equal(LogLevel.Error, logger.LastLogLevel);
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
    }
}
