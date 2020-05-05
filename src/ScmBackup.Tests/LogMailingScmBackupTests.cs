using ScmBackup.Loggers;
using Xunit;

namespace ScmBackup.Tests
{
    public class LogMailingScmBackupTests
    {
        static (FakeEmailSender mail, LogMailingScmBackup sut) BuildFakeLogMailingScmBackup(bool innerReturnValue)
        {
            var subBackup = new FakeScmBackup();
            subBackup.ToReturn = innerReturnValue;

            var messages = new LogMessages();
            messages.AddMessage("1");
            messages.AddMessage("2");

            var mail = new FakeEmailSender();

            var sut = new LogMailingScmBackup(subBackup, messages, mail);
            return (mail, sut);
        }

        [Fact]
        public void RunSendsMail()
        {
            var (mail, sut) = BuildFakeLogMailingScmBackup(true);

            sut.Run();

            Assert.NotNull(mail.LastSubject);
            Assert.NotNull(mail.LastBody);
            Assert.Contains("1", mail.LastBody);
            Assert.Contains("2", mail.LastBody);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RunReturnsValueFromInnerExecution(bool innerReturnValue)
        {
            var (mail, sut) = BuildFakeLogMailingScmBackup(innerReturnValue);

            var result = sut.Run();

            Assert.Equal(result, innerReturnValue);
        }
    }
}
