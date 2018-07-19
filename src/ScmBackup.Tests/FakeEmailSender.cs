using ScmBackup.Http;

namespace ScmBackup.Tests
{
    public class FakeEmailSender : IEmailSender
    {
        public string LastSubject { get; private set; }
        public string LastBody { get; private set; }

        public void Send(string subject, string body)
        {
            this.LastSubject = subject;
            this.LastBody = body;
        }
    }
}
