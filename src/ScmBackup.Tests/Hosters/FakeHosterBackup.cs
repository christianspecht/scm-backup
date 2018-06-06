using ScmBackup.Hosters;
using System;

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

        public override void BackupIssues(string subdir)
        {
            this.issuesWasExecuted = true;
        }

        public override void BackupRepo(string subdir)
        {
            this.repoWasExecuted = true;
        }

        public override void BackupWiki(string subdir)
        {
            this.wikiWasExecuted = true;
        }
    }
}
