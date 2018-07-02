using ScmBackup.Scm;
using System;

namespace ScmBackup.Hosters.Github
{
    internal class GithubBackup : BackupBase
    {
        public GithubBackup(IScmFactory scmfactory)
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

        public override void BackupWiki(string subdir, ScmCredentials credentials)
        {
            InitScm();

            scm.PullFromRemote(this.repo.WikiUrl, subdir, credentials);

            if (!scm.DirectoryIsRepository(subdir))
            {
                throw new InvalidOperationException(Resource.DirectoryNoRepo);
            }
        }
    }
}
