using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Hosters.Gitlab
{
    internal class GitlabApiIssueStats
    {
        public GitlabApiIssueStats_Stats statistics { get; set; }
    }

    internal class GitlabApiIssueStats_Stats
    {
        public GitlabApiIssueStats_Counts counts { get; set; }
    }

    internal class GitlabApiIssueStats_Counts
    {
        public int all { get; set; }
    }
}
