using ScmBackup.Loggers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScmBackup.Tests.Loggers
{
    public class EmailLoggerTests
    {
        [Fact]
        public void SendsMailWithLogs()
        {
            var mail = new FakeEmailSender();
            var sut = new EmailLogger(mail);

            sut.Log(ErrorLevel.Debug, "AAAA");
            sut.Log(ErrorLevel.Info, "BBBB");
            sut.Log(ErrorLevel.Error, "CCCC");
            sut.ExecuteOnExit(true);

            Assert.NotNull(mail.LastSubject);
            Assert.NotNull(mail.LastBody);
            Assert.DoesNotContain("AAAA", mail.LastBody);
            Assert.Contains("BBBB", mail.LastBody);
            Assert.Contains("CCCC", mail.LastBody);
        }
    }
}
