using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScmBackup.Scm
{
    [Scm(Type = ScmType.Git)]
    internal class GitScm : IScm
    {
        public string ShortName
        {
            get { return "git"; }
        }

        public bool IsOnThisComputer(Config config)
        {
            throw new NotImplementedException();
        }
    }
}
