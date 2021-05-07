using ScmBackup.CompositionRoot;
using ScmBackup.Configuration;
using ScmBackup.Tests.Hosters;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests
{
    public class ValidatingConfigReaderTests
    {
        private IConfigReader sut;
        private FakeHosterValidator validator;
        private FakeConfigReader reader;
        private FakeLogger logger;

        public ValidatingConfigReaderTests()
        {
            reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            logger = new FakeLogger();
            validator = new FakeHosterValidator();

            sut = new ValidatingConfigReader(reader, logger, validator);
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

        [Fact]
        public void ExecutesConfigSourceValidation()
        {
            var result = sut.ReadConfig();

            Assert.True(validator.WasValidated);
        }

        [Fact]
        public void ErrorsWhenConfigSourceValidationReturnsError()
        {
            validator.Result.AddMessage(ErrorLevel.Error, "foo");

            var result = sut.ReadConfig();

            Assert.True(logger.LoggedSomething);
            Assert.Equal(ErrorLevel.Error, logger.LastErrorLevel);
            Assert.Equal("foo", logger.LastMessage);
            Assert.Null(result);
        }

        [Fact]
        public void DoesntErrorWhenConfigSourceValidationReturnsWarning()
        {
            validator.Result.AddMessage(ErrorLevel.Warn, "foo");

            var result = sut.ReadConfig();

            Assert.True(logger.LoggedSomething);
            Assert.Equal(ErrorLevel.Warn, logger.LastErrorLevel);
            Assert.Equal("foo", logger.LastMessage);
            Assert.NotNull(result);
        }

        [Fact]
        public void ValidatesOnlyOnceWhenCalledMultipleTimes()
        {
            sut.ReadConfig();
            sut.ReadConfig();
            Assert.Equal(1, validator.ValidationCounter);
        }

        [Fact]
        public void LogsErrorWhenAConfigSourceHasNoTitle()
        {
            reader.FakeConfig.Sources.First().Title = "";

            var result = sut.ReadConfig();

            Assert.True(logger.LoggedSomething);
            Assert.Equal(ErrorLevel.Error, logger.LastErrorLevel);
            Assert.Null(result);
        }

        [Fact]
        public void LogsErrorWhenConfigSourceTitlesContainDuplicates()
        {
            reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            var source = new ConfigSource();
            source.Title = "title";
            source.Type = "fake";

            reader.FakeConfig.Sources.Add(source);

            logger = new FakeLogger();
            validator = new FakeHosterValidator();

            sut = new ValidatingConfigReader(reader, logger, validator);
            var result = sut.ReadConfig();

            Assert.True(logger.LoggedSomething);
            Assert.Equal(ErrorLevel.Error, logger.LastErrorLevel);
            Assert.Null(result);
        }
    }
}
