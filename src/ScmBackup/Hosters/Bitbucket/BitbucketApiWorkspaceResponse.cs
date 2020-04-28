using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Hosters.Bitbucket
{
    internal class BitbucketApiWorkspaceResponse
    {
        public Dictionary<string, Dictionary<string, string>> links { get; set; }
    }
}
