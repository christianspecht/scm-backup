using ScmBackup.Hosters;
using ScmBackup.Scm;

namespace ScmBackup.Tests.Hosters
{
    internal class FakeHosterBackup : BackupBase
    {
        private bool issuesWasExecuted;
        private bool repoWasExecuted;
        private bool wikiWasExecuted;

        public FakeHosterBackup()
        {
            this.scmFactory = new FakeScmFactory();
        }

        public bool WasExecuted
        {
            get
            {
                return this.issuesWasExecuted && this.repoWasExecuted && this.wikiWasExecuted;
            }
        }

        public override void BackupIssues(string subdir, ScmCredentials credentials)
        {
            this.issuesWasExecuted = true;
        }

        public override void BackupRepo(string subdir, ScmCredentials credentials)
        {
            this.repoWasExecuted = true;
        }

        public override void BackupWiki(string subdir, ScmCredentials credentials)
        {
            this.wikiWasExecuted = true;
        }
    }
}
