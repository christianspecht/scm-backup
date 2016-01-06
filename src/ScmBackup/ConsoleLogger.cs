using System;

namespace ScmBackup
{
    /// <summary>
    /// Logs to the console
    /// </summary>
    internal class ConsoleLogger : ILogger
    {
        public void Log(LogLevel level, string message, params object[] arg)
        {
            this.Log(level, null, message, arg);
        }

        public void Log(LogLevel level, Exception ex, string message, params object[] arg)
        {
            switch (level)
            {
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Error:
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
    }
}
