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

        public override void BackupRepo(string subdir)
        {
            this.InitScm();

            this.scm.PullFromRemote(this.repo.CloneUrl, subdir);

            if (!this.scm.DirectoryIsRepository(subdir))
            {
                throw new InvalidOperationException(Resource.DirectoryNoRepo);
            }
        }

        public override void BackupWiki(string subdir)
        {
            this.InitScm();

            this.scm.PullFromRemote(this.repo.WikiUrl, subdir);

            if (!this.scm.DirectoryIsRepository(subdir))
            {
                throw new InvalidOperationException(Resource.DirectoryNoRepo);
            }
        }
    }
}
