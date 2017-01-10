using ScmBackup.Scm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScmBackup
{
    /// <summary>
    /// main program execution
    /// </summary>
    internal class ScmBackup : IScmBackup
    {
        private readonly IConfigReader reader;
        private readonly IApiCaller apiCaller;
        private readonly IScmValidator validator;

        public ScmBackup(IConfigReader reader, IApiCaller apiCaller, IScmValidator validator)
        {
            this.reader = reader;
            this.apiCaller = apiCaller;
            this.validator = validator;
        }

        public void Run()
        {
            var config = this.reader.ReadConfig();

            var repos = this.apiCaller.CallApis(config);

            if (!this.validator.ValidateScms(repos.GetScmTypes(), config))
            {
                throw new InvalidOperationException(Resource.ScmValidatorError);
            }

            throw new NotImplementedException();
        }
    }
}
