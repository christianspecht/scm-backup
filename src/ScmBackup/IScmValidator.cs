using System.Collections.Generic;

namespace ScmBackup
{
    /// <summary>
    /// Verifies that all passed SCMs are present on this machine
    /// </summary>
    internal interface IScmValidator
    {
        bool ValidateScms(HashSet<ScmType> scms, Config config);
    }
}
