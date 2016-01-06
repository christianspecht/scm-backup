using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScmBackup
{
    internal class ScmBackup : IScmBackup
    {
        private ILogger logger;

        public ScmBackup(ILogger logger)
        {
            this.logger = logger;
        }

        public void Run()
        {
            this.logger.Log(LogLevel.Info, "SCM Backup");
        }
    }
}
