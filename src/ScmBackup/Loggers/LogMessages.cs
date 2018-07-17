using System.Collections.Generic;

namespace ScmBackup.Loggers
{
    internal class LogMessages : ILogMessages
    {
        private List<string> messages = new List<string>();

        public void AddMessage(string message)
        {
            this.messages.Add(message);
        }

        public IEnumerable<string> GetMessages()
        {
            return this.messages;
        }
    }
}
