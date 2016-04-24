using System;
using Xunit;


namespace ScmBackup.Tests
{
    public class ErrorHandlingScmBackupTests
    {
        [Fact]
        public void LogsWhenExceptionIsThrown()
        {
            var ex = new Exception("!!!");
            var subBackup = new FakeScmBackup();
            subBackup.ToThrow = ex;

            var conf = new FakeConfigReader();
            conf.SetDefaultFakeConfig();

            var logger = new FakeLogger();

            var backup = new ErrorHandlingScmBackup(subBackup, logger, conf);
            backup.Run();

            Assert.True(logger.LoggedSomething);
            Assert.Equal<ErrorLevel>(ErrorLevel.Error, logger.LastErrorLevel);
            // we can't check whether the last exception is the exception from above,
            // because there are more logging outputs after the exception.
        }

        [Fact]
        public void LogsWhenConfigIsNull()
        {
            var subBackup = new FakeScmBackup();

            var conf = new FakeConfigReader();
            conf.FakeConfig = null;

            var logger = new FakeLogger();

            var backup = new ErrorHandlingScmBackup(subBackup, logger, conf);
            backup.WaitSecondsOnError = 0;
            backup.Run();

            Assert.True(logger.LoggedSomething);
            Assert.Equal<ErrorLevel>(ErrorLevel.Error, logger.LastErrorLevel);
        }
    }
}
