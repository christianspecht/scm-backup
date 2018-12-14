using System;
using System.Collections.Generic;

namespace ScmBackup.Loggers
{
    /// <summary>
    /// Logs to the console
    /// </summary>
    internal class ConsoleLogger : ILogger
    {
        public void Log(ErrorLevel level, string message, params object[] arg)
        {
            this.Log(level, null, message, arg);
        }

        public void Log(ErrorLevel level, Exception ex, string message, params object[] arg)
        {
            switch (level)
            {
                case ErrorLevel.Debug:
                    return;
                case ErrorLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ErrorLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            if (ex != null)
            {
                message += " " + ex.Message;
            }

            Console.WriteLine(message, arg);
            Console.ResetColor();
        }

        public List<string> FilesToBackup
        {
            get { return null; }
        }

        public void ExecuteOnExit()
        {
        }
    }
}
