using System;
using System.Collections.Generic;
using System.Linq;

namespace ScmBackup.Loggers
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

        public List<string> FilesToBackup
        {
            get
            {
                var list = new List<string>();
                foreach (var logger in this.loggers)
                {
                    if (logger.FilesToBackup != null && logger.FilesToBackup.Any())
                    {
                        list.AddRange(logger.FilesToBackup);
                    }
                }

                return list;
            }
        }

        public void ExecuteOnExit(bool successful)
        {
            foreach (var logger in this.loggers)
            {
                logger.ExecuteOnExit(successful);
            }
        }
    }
}
