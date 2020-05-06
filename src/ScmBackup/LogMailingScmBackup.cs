using ScmBackup.Http;
using ScmBackup.Loggers;
using System;
using System.Linq;

namespace ScmBackup
{
    /// <summary>
    /// Sends the console output via mail
    /// </summary>
    internal class LogMailingScmBackup : IScmBackup
    {
        private readonly IScmBackup backup;
        private readonly ILogMessages messages;
        private readonly IEmailSender mail;

        public LogMailingScmBackup(IScmBackup backup, ILogMessages messages, IEmailSender mail)
        {
            this.backup = backup;
            this.messages = messages;
            this.mail = mail;
        }

        public bool Run()
        {
            var result = this.backup.Run();
            string success = result ? Resource.LogMailSubjectSuccess : Resource.LogMailSubjectFailed;

            string subject = string.Format(Resource.LogMailSubject, success, DateTime.Now.ToString("dd MMM HH:mm:ss"));
            string body = string.Join(Environment.NewLine, this.messages.GetMessages().ToArray());

            this.mail.Send(subject, body);

            return result;
        }
    }
}
