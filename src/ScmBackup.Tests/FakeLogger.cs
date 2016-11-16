using System;

namespace ScmBackup.Tests
{
    internal class FakeLogger : ILogger
    {
        public bool LoggedSomething { get; set; }
        public ErrorLevel LastErrorLevel { get; set; }
        public Exception LastException { get; set; }
        public string LastMessage { get; set; }
        public object[] LastArg { get; set; }

        public bool IgnoreDebugLogs { get; set; }
        public bool ConsoleOutput { get; set; }

        public FakeLogger()
        {
            // default setting: ignore all debug logs, because they "get in the way" 
            // when checking whether the last log was an error, for example
            this.IgnoreDebugLogs = true;

            // default setting: don't actually output log messages
            this.ConsoleOutput = false;
        }

        public void Log(ErrorLevel level, string message, params object[] arg)
        {
            this.Log(level, null, message, arg);
        }

        public void Log(ErrorLevel level, Exception ex, string message, params object[] arg)
        {
            if (level == ErrorLevel.Debug && this.IgnoreDebugLogs)
            {
                return;
            }

            if (this.ConsoleOutput)
            {
                Console.WriteLine(string.Format("[{0}] ", level) + string.Format(message, arg));
            }

            this.LoggedSomething = true;
            this.LastErrorLevel = level;
            this.LastException = ex;
            this.LastMessage = message;
            this.LastArg = arg;
        }
    }
}
