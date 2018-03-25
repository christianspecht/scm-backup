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
                if (!this.scm.IsOnThisComputer())
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

            // issue #13: the GitHub API only returns whether it's *possible* to create a wiki, but not if the repo actually *has* a wiki.
            // So we need to skip the wiki when the URL (which we built manually) is not a valid repository:
            if (!scm.RemoteRepositoryExists(this.repo.WikiUrl))
            {
                return;
            }

            scm.PullFromRemote(this.repo.WikiUrl, subdir);

            if (!scm.DirectoryIsRepository(subdir))
            {
                throw new InvalidOperationException(Resource.DirectoryNoRepo);
            }
        }
    }
}
