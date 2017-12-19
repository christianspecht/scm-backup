using ScmBackup.Scm;
using System;

namespace ScmBackup.Hosters.Github
{
    internal class GithubBackup : BackupBase
    {
        private readonly IScmFactory scmFactory;
        private IScm scm;

        public GithubBackup(IScmFactory scmfactory)
        {
            this.scmFactory = scmfactory;
        }

        public void InitScm()
        {
            if (this.scm == null)
            {
                this.scm = this.scmFactory.Create(this.repo.Scm);
                if (!this.scm.IsOnThisComputer(this.config))
                {
                    throw new InvalidOperationException(string.Format(Resource.ScmNotOnThisComputer, this.repo.Scm.ToString()));
                }
            }
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
