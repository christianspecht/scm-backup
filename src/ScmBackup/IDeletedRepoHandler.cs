using ScmBackup.Hosters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup
{
    internal interface IDeletedRepoHandler
    {
        IEnumerable<string> HandleDeletedRepos(IEnumerable<HosterRepository> repos, string backupDir);
    }
}
