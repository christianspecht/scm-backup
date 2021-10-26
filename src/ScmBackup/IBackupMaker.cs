using ScmBackup.Configuration;
using ScmBackup.Hosters;
using System.Collections.Generic;

namespace ScmBackup
{
    internal interface IBackupMaker
    {
        string Backup(ConfigSource source, IEnumerable<HosterRepository> repos);
    }
}
