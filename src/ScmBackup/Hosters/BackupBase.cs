using ScmBackup.Configuration;
using ScmBackup.Scm;
using System;
using System.IO;

namespace ScmBackup.Hosters
{
    internal abstract class BackupBase : IBackup
    {
        public readonly string SubDirRepo = "repo";
        public readonly string SubDirWiki = "wiki";
        public readonly string SubDirIssues = "issues";

        protected HosterRepository repo;
        protected IScm scm;

        // this MUST be filled in the child classes' constructor
        public IScmFactory scmFactory;

        public void MakeBackup(ConfigSource source, HosterRepository repo, string repoFolder)
        {
            if (this.scmFactory == null)
            {
                throw new ArgumentNullException("!!");
            }

            ScmCredentials credentials = null;
            if (repo.IsPrivate)
            {
                credentials = new ScmCredentials(source.AuthName, source.Password);
            }

            this.repo = repo;

            string subdir = Path.Combine(repoFolder, this.SubDirRepo);
            this.BackupRepo(subdir, credentials);

            if (this.repo.HasWiki)
            {
                subdir = Path.Combine(repoFolder, this.SubDirWiki);
                this.BackupWiki(subdir, credentials);
            }

            if (this.repo.HasIssues)
            {
                subdir = Path.Combine(repoFolder, this.SubDirIssues);
                this.BackupIssues(subdir, credentials);
            }
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

        // this MUST be implemented in the child classes
        public abstract void BackupRepo(string subdir, ScmCredentials credentials);

        // these can be implemented in the child classes IF the given hoster has issues, a wiki...
        public virtual void BackupWiki(string subdir, ScmCredentials credentials) { }
        public virtual void BackupIssues(string subdir, ScmCredentials credentials) { }
    }
}
