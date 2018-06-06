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

        public override void BackupRepo(string subdir)
        {
            InitScm();

            scm.PullFromRemote(this.repo.CloneUrl, subdir);

            if (!scm.DirectoryIsRepository(subdir))
            {
                throw new InvalidOperationException(Resource.DirectoryNoRepo);
            }
        }

        public override void BackupWiki(string subdir)
        {
            InitScm();

            scm.PullFromRemote(this.repo.WikiUrl, subdir);

            if (!scm.DirectoryIsRepository(subdir))
            {
                throw new InvalidOperationException(Resource.DirectoryNoRepo);
            }
        }
    }
}
