using ScmBackup.Scm;
using System.Collections.Generic;

namespace ScmBackup
{
    /// <summary>
    /// Verifies that all passed SCMs are present on this machine
    /// </summary>
    internal class ScmValidator : IScmValidator
    {
        private readonly IScmFactory factory;
        private readonly ILogger logger;

        public ScmValidator(IScmFactory factory, ILogger logger)
        {
            this.factory = factory;
            this.logger = logger;
        }

        public bool ValidateScms(HashSet<ScmType> scms, Config config)
        {
            bool ok = true;

            foreach (var scmType in scms)
            {
                var scm = this.factory.Create(scmType);
                
                if (scm.IsOnThisComputer(config))
                {
                    this.logger.Log(ErrorLevel.Info, Resource.ScmOnThisComputer, scm.ShortName);
                }
                else
                {
                    this.logger.Log(ErrorLevel.Error, Resource.ScmNotOnThisComputer, scm.ShortName);
                    ok = false;
                }
            }

            return ok;
        }
    }
}
