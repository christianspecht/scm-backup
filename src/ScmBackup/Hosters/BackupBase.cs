using System.IO;

namespace ScmBackup.Hosters
{
    internal abstract class BackupBase : IBackup
    {
        public readonly string SubDirRepo = "repo";
        public readonly string SubDirWiki = "wiki";
        public readonly string SubDirIssues = "issues";

        protected HosterRepository repo;
        protected Config config;

        public void MakeBackup(HosterRepository repo, Config config, string repoFolder)
        {
            this.repo = repo;
            this.config = config;

            string subdir = Path.Combine(repoFolder, this.SubDirRepo);
            this.BackupRepo(subdir);

            subdir = Path.Combine(repoFolder,this.SubDirWiki);
            this.BackupWiki(subdir);

            subdir = Path.Combine(repoFolder, this.SubDirIssues);
            this.BackupIssues(subdir);
        }

        // this MUST be implemented in the child classes
        public abstract void BackupRepo(string subdir);

        // these can be implemented in the child classes IF the given hoster has issues, a wiki...
        public virtual void BackupWiki(string subdir) { }
        public virtual void BackupIssues(string subdir) { }
    }
}
