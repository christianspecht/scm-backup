using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScmBackup.Scm
{
    internal interface IScm
    {
        string ShortName { get; }
    }
}
