using System;
using Xunit;


namespace ScmBackup.Tests
{
    public class ErrorHandlingScmBackupTests
    {
        static (FakeLogger FakeLogger, ErrorHandlingScmBackup ErrorHandlingScmBackup) BuildFakeScmBackup()
        {
            var ex = new Exception("!!!");
            var subBackup = new FakeScmBackup();
            subBackup.ToThrow = ex;

            var conf = new FakeConfigReader();
            conf.SetDefaultFakeConfig();

            var context = new FakeContext();

            var logger = new FakeLogger();

            var backup = new ErrorHandlingScmBackup(subBackup, logger, context);
            return (logger, backup);
        }

        [Fact]
        public void LogsWhenExceptionIsThrown()
        {
            var (logger, backup) = BuildFakeScmBackup();

            backup.Run();

            Assert.True(logger.LoggedSomething);
            Assert.Equal(ErrorLevel.Error, logger.LastErrorLevel);
            // we can't check whether the last exception is the exception from above,
            // because there are more logging outputs after the exception.
        }
    }
}
