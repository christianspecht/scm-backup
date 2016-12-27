using System;

namespace ScmBackup.Scm
{
    internal class ScmAttribute : Attribute
    {
        public ScmType Type { get; set; }
    }
}
