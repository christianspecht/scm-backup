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

        [Fact]
        public void ReturnsFalseWhenExceptionIsThrown()
        {
            var (_, backup) = BuildFakeScmBackup();

            var result = backup.Run();

            Assert.False(result);
        }

        [Fact]
        public void RunsExecuteOnExit_OnRegularExit()
        {
            var subBackup = new FakeScmBackup();
            var context = new FakeContext();
            var logger = new FakeLogger();

            var sut = new ErrorHandlingScmBackup(subBackup, logger, context);
            sut.Run();

            Assert.True(logger.ExecutedOnExit);
            Assert.True(logger.ExecuteOnExit_Successful);
        }

        [Fact]
        public void RunsExecuteOnExit_WhenExceptionIsThrown()
        {
            var subBackup = new FakeScmBackup();
            subBackup.ToThrow = new Exception("!!");

            var context = new FakeContext();
            var logger = new FakeLogger();

            var sut = new ErrorHandlingScmBackup(subBackup, logger, context);
            sut.Run();

            Assert.True(logger.ExecutedOnExit);
            Assert.False(logger.ExecuteOnExit_Successful);
        }
    }
}
