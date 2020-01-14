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
            InitScm();
            scm.PullFromRemote(this.repo.CloneUrl, subdir, credentials);

            if (!scm.DirectoryIsRepository(subdir))
            {
                throw new InvalidOperationException(Resource.DirectoryNoRepo);
            }
        }
    }
}
