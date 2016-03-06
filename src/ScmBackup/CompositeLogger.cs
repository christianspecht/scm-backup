using System;
using System.Collections.Generic;

namespace ScmBackup
{
    /// <summary>
    /// Wrapper, calls the other loggers
    /// </summary>
    internal class CompositeLogger : ILogger
    {
        private IEnumerable<ILogger> loggers;

        public CompositeLogger(IEnumerable<ILogger> loggers)
        {
            this.loggers = loggers;
        }

        public void Log(ErrorLevel level, string message, params object[] arg)
        {
            this.Log(level, null, message, arg);
        }

        public void Log(ErrorLevel level, Exception ex, string message, params object[] arg)
        {
            foreach (var logger in this.loggers)
            {
                logger.Log(level, ex, message, arg);
            }
        }
    }
}
