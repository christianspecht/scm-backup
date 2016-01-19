using System;

namespace ScmBackup.Tests
{
    internal class FakeLogger : ILogger
    {
        public bool LoggedSomething { get; set; }
        public LogLevel LastLogLevel { get; set; }
        public Exception LastException { get; set; }
        public string LastMessage { get; set; }
        public object[] LastArg { get; set; }

        public void Log(LogLevel level, string message, params object[] arg)
        {
            this.Log(level, null, message, arg);
        }

        public void Log(LogLevel level, Exception ex, string message, params object[] arg)
        {
            this.LoggedSomething = true;
            this.LastLogLevel = level;
            this.LastException = ex;
            this.LastMessage = message;
            this.LastArg = arg;
        }
    }
}
