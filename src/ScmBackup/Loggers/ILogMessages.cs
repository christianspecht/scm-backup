using System.Collections.Generic;

namespace ScmBackup.Loggers
{
    internal interface ILogMessages
    {
        void AddMessage(string message);
        IEnumerable<string> GetMessages();
    }
}
