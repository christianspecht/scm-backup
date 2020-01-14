using System;
using System.Collections.Generic;
using System.Text;
using ScmBackup.Scm;

namespace ScmBackup.Hosters.Gitlab
{
    internal class GitlabBackup : BackupBase
    {
        public GitlabBackup(IScmFactory scmfactory)
        {
            this.scmFactory = scmfactory;
        }

        public override void BackupRepo(string subdir, ScmCredentials credentials)
        {
            this.DefaultBackup(this.repo.CloneUrl, subdir, credentials);
        }

        public override void BackupWiki(string subdir, ScmCredentials credentials)
        {
            this.DefaultBackup(this.repo.WikiUrl, subdir, credentials);
        }
    }
}
