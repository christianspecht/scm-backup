using System;
using System.Collections.Generic;
using System.Text;
using ScmBackup.Scm;

namespace ScmBackup.Hosters.Gitlab
{
    internal class GitlabBackup : BackupBase
    {
        public override void BackupRepo(string subdir, ScmCredentials credentials)
        {
            throw new NotImplementedException();
        }
    }
}
