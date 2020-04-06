using ScmBackup.Scm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Hosters.Bitbucket
{
    internal class BitbucketBackup : BackupBase
    {
        public BitbucketBackup(IScmFactory scmfactory)
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
