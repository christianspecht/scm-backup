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
            this.DefaultBackup(this.repo.CloneUrl, subdir, credentials);
        }

        public override void BackupWiki(string subdir, ScmCredentials credentials)
        {
            this.DefaultBackup(this.repo.WikiUrl, subdir, credentials);
        }
    }
}
