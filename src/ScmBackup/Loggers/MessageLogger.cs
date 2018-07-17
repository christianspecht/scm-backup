using System;
using System.Text;

namespace ScmBackup.Loggers
{
    internal class MessageLogger : ILogger
    {
        private ILogMessages messages;

        public MessageLogger(ILogMessages messages)
        {
            this.messages = messages;
        }

        public void Log(ErrorLevel level, string message, params object[] arg)
        {
            this.Log(level, null, message, arg);
        }

        public void Log(ErrorLevel level, Exception ex, string message, params object[] arg)
        {
            var tmp = new StringBuilder();
            tmp.Append(level.ToString("f")); // https://stackoverflow.com/a/32726578/6884

            if (ex != null)
            {
                tmp.Append(" ");
                tmp.Append(ex.Message);
            }

            tmp.Append(" ");
            tmp.AppendFormat(message, arg);

            this.messages.AddMessage(tmp.ToString());
        }
    }
}
