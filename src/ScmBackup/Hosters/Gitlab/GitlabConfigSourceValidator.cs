using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Hosters.Gitlab
{
    /// <summary>
    /// validator for GitLab config sources
    /// </summary>
    internal class GitlabConfigSourceValidator : ConfigSourceValidatorBase
    {
        public override string HosterName
        {
            get { return "gitlab"; }
        }

        public override bool AuthNameAndNameMustBeEqual
        {
            get { return true; }
        }
    }
}
