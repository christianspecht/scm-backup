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
            Assert.Equal(ex, logger.LastException);
        }
    }
}
