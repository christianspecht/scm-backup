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
            this.InitScm();

            this.scm.PullFromRemote(this.repo.CloneUrl, subdir, credentials);

            if (!this.scm.DirectoryIsRepository(subdir))
            {
                throw new InvalidOperationException(Resource.DirectoryNoRepo);
            }
        }

        public override void BackupWiki(string subdir, ScmCredentials credentials)
        {
            this.InitScm();

            this.scm.PullFromRemote(this.repo.WikiUrl, subdir, credentials);

            if (!this.scm.DirectoryIsRepository(subdir))
            {
                throw new InvalidOperationException(Resource.DirectoryNoRepo);
            }
        }
    }
}
