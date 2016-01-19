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
            var stub = new FakeScmBackup();
            stub.ToThrow = ex;

            var mock = new FakeLogger();

            var backup = new ErrorHandlingScmBackup(stub, mock);
            backup.Run();

            Assert.True(mock.LoggedSomething);
            Assert.Equal<LogLevel>(LogLevel.Error, mock.LastLogLevel);
            Assert.Equal(ex, mock.LastException);
        }
    }
}
