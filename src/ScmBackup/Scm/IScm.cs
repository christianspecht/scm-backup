using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScmBackup.Scm
{
    internal interface IScm
    {
        /// <summary>
        /// Short name of the SCM
        /// </summary>
        string ShortName { get; }

        /// <summary>
        /// check whether the SCM is present on this computer
        /// </summary>
        bool IsOnThisComputer();
    }
}
