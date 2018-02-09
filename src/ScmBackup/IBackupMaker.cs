using ScmBackup.Hosters;
using System.Collections.Generic;

namespace ScmBackup
{
    internal interface IBackupMaker
    {
        void Backup(ConfigSource source, IEnumerable<HosterRepository> repos);
    }
}
