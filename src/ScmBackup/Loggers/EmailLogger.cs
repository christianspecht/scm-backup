using ScmBackup.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Loggers
{
   /// <summary>
   /// Sends log via email
   /// </summary>
    class EmailLogger : ILogger
    {
        private readonly IEmailSender mail;
        private List<string> messages;

        public EmailLogger(IEmailSender mail)
        {
            this.mail = mail;
            this.messages = new List<string>();
        }
        public List<string> FilesToBackup
        {
            get { return null; }
        }

        public void ExecuteOnExit(bool successful)
        {
            string success = successful ? Resource.LogMailSubjectSuccess : Resource.LogMailSubjectFailed;

            string subject = string.Format("!" + Resource.LogMailSubject, success, DateTime.Now.ToString("dd MMM HH:mm:ss"));
            string body = string.Join(Environment.NewLine, this.messages);

            this.mail.Send(subject, body);
        }

        public void Log(ErrorLevel level, string message, params object[] arg)
        {
            this.Log(level, null, message, arg);
        }

        public void Log(ErrorLevel level, Exception ex, string message, params object[] arg)
        {
            if (level == ErrorLevel.Debug)
            {
                return;
            }

            var tmp = new StringBuilder();
            tmp.Append(level.LevelName());

            if (ex != null)
            {
                tmp.Append(" ");
                tmp.Append(ex.Message);
            }

            tmp.Append(" ");
            tmp.AppendFormat(message, arg);

            this.messages.Add(tmp.ToString());
        }
    }
}
