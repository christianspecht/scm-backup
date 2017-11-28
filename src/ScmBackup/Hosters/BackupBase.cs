using System.IO;

namespace ScmBackup.Hosters
{
    internal abstract class BackupBase : IBackup
    {
        private HosterRepository repo;
        private Config config;

        public void MakeBackup(HosterRepository repo, Config config, string repoFolder)
        {
            this.repo = repo;
            this.config = config;

            string subdir = Path.Combine(repoFolder, "repo");
            this.BackupRepo(subdir);

            subdir = Path.Combine(repoFolder, "wiki");
            this.BackupWiki(subdir);

            subdir = Path.Combine(repoFolder, "issues");
            this.BackupIssues(subdir);
        }

        // this MUST be implemented in the child classes
        public abstract void BackupRepo(string subdir);

        // these can be implemented in the child classes IF the given hoster has issues, a wiki...
        public virtual void BackupWiki(string subdir) { }
        public virtual void BackupIssues(string subdir) { }
    }
}
